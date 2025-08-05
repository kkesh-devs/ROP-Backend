using KKESH_ROP.DTO.JoinMedicalCenter;
using KKESH_ROP.DTO.MedicalCenter;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JoinMedicalCenterController : ControllerBase
{
    private readonly IJoinMedicalCenterRepository _joinMedicalCenterRepository;
    private readonly IMedicalCenterRepository _medicalCenterRepository;

    public JoinMedicalCenterController(IJoinMedicalCenterRepository joinMedicalCenterRepository, IMedicalCenterRepository medicalCenterRepository)
    {
        _joinMedicalCenterRepository = joinMedicalCenterRepository;
        _medicalCenterRepository = medicalCenterRepository;
    }

    /// <summary>
    /// Get all join medical center requests - Used by admins to view all requests regardless of status
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _joinMedicalCenterRepository.GetAllAsync();
        return Ok(response);
    }

    /// <summary>
    /// Get a specific join request by ID - Used to view details of a particular request
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _joinMedicalCenterRepository.GetByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    /// <summary>
    /// Get join request by user ID - Used to check if a user has an existing request or view their request status
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var response = await _joinMedicalCenterRepository.GetByUserIdAsync(userId);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    /// <summary>
    /// Get all requests by specific status - Used by admins to filter requests by status
    /// </summary>
    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(JoinMedicalCenterStatus status)
    {
        var response = await _joinMedicalCenterRepository.GetByStatusAsync(status);
        return Ok(response);
    }

    /// <summary>
    /// Get all pending requests - Used by admins to view new requests that need initial processing
    /// </summary>
    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingRequests()
    {
        var response = await _joinMedicalCenterRepository.GetPendingRequestsAsync();
        return Ok(response);
    }

    /// <summary>
    /// Get all integration requests - Used by integration team to view requests in integration phase
    /// </summary>
    [HttpGet("integration")]
    public async Task<IActionResult> GetIntegrationRequests()
    {
        var response = await _joinMedicalCenterRepository.GetByStatusAsync(JoinMedicalCenterStatus.Integration);
        return Ok(response);
    }

    /// <summary>
    /// Get all validation requests - Used by validation team to view requests in validation phase
    /// </summary>
    [HttpGet("validation")]
    public async Task<IActionResult> GetValidationRequests()
    {
        var response = await _joinMedicalCenterRepository.GetByStatusAsync(JoinMedicalCenterStatus.Validation);
        return Ok(response);
    }

    /// <summary>
    /// Create a new join medical center request - Used by medical centers to request joining the platform
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJoinMedicalCenterRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await _joinMedicalCenterRepository.CreateAsync(dto);
        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Process a join request with status change - Used by admins to change request status and automatically update medical center status when approved/rejected
    /// </summary>
    [HttpPut("{id}/process")]
    public async Task<IActionResult> Process(string id, [FromBody] ProcessJoinMedicalCenterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        // Process the join request
        var processResponse = await _joinMedicalCenterRepository.ProcessAsync(id, dto);
        if (!processResponse.Success)
            return NotFound(processResponse);

        // If approved, update the medical center status to Approved
        if (dto.Status == JoinMedicalCenterStatus.Approved)
        {
            var joinRequestResponse = await _joinMedicalCenterRepository.GetByIdAsync(id);
            if (joinRequestResponse.Success)
            {
                // Get the medical center by userId first
                var medicalCenterResponse = await _medicalCenterRepository.GetByUserIdAsync(joinRequestResponse.Data.UserId);
                if (medicalCenterResponse.Success)
                {
                    // Update medical center status to Approved
                    var updateMedicalCenterDto = new UpdateMedicalCenterDto
                    {
                        Status = MedicalCenterStatus.Approved
                    };

                    var medicalCenterUpdateResponse = await _medicalCenterRepository.UpdateAsync(medicalCenterResponse.Data.Id, updateMedicalCenterDto);
                    
                    return Ok(new Response<object>(true, 
                        "Join request approved and medical center activated successfully", 
                        new { JoinRequest = processResponse, MedicalCenterUpdate = medicalCenterUpdateResponse }));
                }
            }
        }
        else if (dto.Status == JoinMedicalCenterStatus.Rejected)
        {
            var joinRequestResponse = await _joinMedicalCenterRepository.GetByIdAsync(id);
            if (joinRequestResponse.Success)
            {
                // Get the medical center by userId first
                var medicalCenterResponse = await _medicalCenterRepository.GetByUserIdAsync(joinRequestResponse.Data.UserId);
                if (medicalCenterResponse.Success)
                {
                    // Update medical center status to Rejected
                    var updateMedicalCenterDto = new UpdateMedicalCenterDto
                    {
                        Status = MedicalCenterStatus.Rejected
                    };

                    var medicalCenterUpdateResponse = await _medicalCenterRepository.UpdateAsync(medicalCenterResponse.Data.Id, updateMedicalCenterDto);
                    
                    return Ok(new Response<object>(true, 
                        "Join request rejected and medical center status updated", 
                        new { JoinRequest = processResponse, MedicalCenterUpdate = medicalCenterUpdateResponse }));
                }
            }
        }

        return Ok(processResponse);
    }

    /// <summary>
    /// Move request to integration phase - Used by admins to advance pending requests to integration team
    /// </summary>
    [HttpPut("{id}/move-to-integration")]
    public async Task<IActionResult> MoveToIntegration(string id, [FromBody] ProcessJoinMedicalCenterActionDto dto)
    {
        var processDto = new ProcessJoinMedicalCenterDto
        {
            Status = JoinMedicalCenterStatus.Integration,
            ProcessedBy = dto.ProcessedBy
        };
        return await Process(id, processDto);
    }

    /// <summary>
    /// Move request to validation phase - Used by integration team to advance requests to validation team
    /// </summary>
    [HttpPut("{id}/move-to-validation")]
    public async Task<IActionResult> MoveToValidation(string id, [FromBody] ProcessJoinMedicalCenterActionDto dto)
    {
        var processDto = new ProcessJoinMedicalCenterDto
        {
            Status = JoinMedicalCenterStatus.Validation,
            ProcessedBy = dto.ProcessedBy
        };
        return await Process(id, processDto);
    }

    /// <summary>
    /// Approve a join request - Used by validation team to approve requests and activate medical center
    /// </summary>
    [HttpPut("{id}/approve")]
    public async Task<IActionResult> Approve(string id, [FromBody] ProcessJoinMedicalCenterActionDto dto)
    {
        var processDto = new ProcessJoinMedicalCenterDto
        {
            Status = JoinMedicalCenterStatus.Approved,
            ProcessedBy = dto.ProcessedBy
        };
        return await Process(id, processDto);
    }

    /// <summary>
    /// Reject a join request - Used by any team to reject requests and update medical center status to rejected
    /// </summary>
    [HttpPut("{id}/reject")]
    public async Task<IActionResult> Reject(string id, [FromBody] ProcessJoinMedicalCenterActionDto dto)
    {
        var processDto = new ProcessJoinMedicalCenterDto
        {
            Status = JoinMedicalCenterStatus.Rejected,
            ProcessedBy = dto.ProcessedBy,
            RejectionReason = dto.Comments
        };
        return await Process(id, processDto);
    }

    /// <summary>
    /// Delete a join request - Used by admins to remove requests from the system (use with caution)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _joinMedicalCenterRepository.DeleteAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
}
