using KKESH_ROP.DTO.JoinPhysician;
using KKESH_ROP.DTO.Physician;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JoinPhysicianController : ControllerBase
{
    private readonly IJoinPhysicianRepository _joinPhysicianRepository;
    private readonly IPhysicianRepository _physicianRepository;

    public JoinPhysicianController(IJoinPhysicianRepository joinPhysicianRepository, IPhysicianRepository physicianRepository)
    {
        _joinPhysicianRepository = joinPhysicianRepository;
        _physicianRepository = physicianRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _joinPhysicianRepository.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _joinPhysicianRepository.GetByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var response = await _joinPhysicianRepository.GetByUserIdAsync(userId);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(JoinPhysicianStatus status)
    {
        var response = await _joinPhysicianRepository.GetByStatusAsync(status);
        return Ok(response);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingRequests()
    {
        var response = await _joinPhysicianRepository.GetPendingRequestsAsync();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJoinPhysicianRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await _joinPhysicianRepository.CreateAsync(dto);
        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateJoinPhysicianDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await _joinPhysicianRepository.UpdateAsync(id, dto);
        if (!response.Success) return NotFound(response);

        return Ok(response);
    }

    [HttpPut("{id}/process")]
    public async Task<IActionResult> Process(string id, [FromBody] ProcessJoinPhysicianDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        // Process the join request
        var processResponse = await _joinPhysicianRepository.ProcessAsync(id, dto);
        if (!processResponse.Success)
            return NotFound(processResponse);

        // If approved, update the physician status to Active
        if (dto.Status == JoinPhysicianStatus.Approved)
        {
            var joinRequestResponse = await _joinPhysicianRepository.GetByIdAsync(id);
            if (joinRequestResponse.Success)
            {
                // Update physician status to Active
                var updatePhysicianDto = new UpdatePhysicianDto
                {
                    Status = PhysicianStatus.Active
                };

                var physicianUpdateResponse = await _physicianRepository.UpdateByUserIdAsync(joinRequestResponse.Data.UserId, updatePhysicianDto);
                
                return Ok(new Response<object>(true, 
                    "Join request approved and physician activated successfully", 
                    new { JoinRequest = processResponse, PhysicianUpdate = physicianUpdateResponse }));
            }
        }
        else if (dto.Status == JoinPhysicianStatus.Rejected)
        {
            var joinRequestResponse = await _joinPhysicianRepository.GetByIdAsync(id);
            if (joinRequestResponse.Success)
            {
                // Update physician status to Rejected
                var updatePhysicianDto = new UpdatePhysicianDto
                {
                    Status = PhysicianStatus.Rejected
                };

                var physicianUpdateResponse = await _physicianRepository.UpdateByUserIdAsync(joinRequestResponse.Data.UserId, updatePhysicianDto);
                
                return Ok(new Response<object>(true, 
                    "Join request rejected and physician status updated", 
                    new { JoinRequest = processResponse, PhysicianUpdate = physicianUpdateResponse }));
            }
        }

        return Ok(processResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _joinPhysicianRepository.DeleteAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
}
