using KKESH_ROP.DTO.DiagnoseRequest;
using KKESH_ROP.Enums;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnoseRequestController(IDiagnoseRequestRepository diagnoseRequestRepository) : ControllerBase
{
    
//____________________________________________________________________________________________________________________________________________________

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await diagnoseRequestRepository.GetAllAsync();
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await diagnoseRequestRepository.GetByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response); 
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("children/{parentId}")]
    public async Task<IActionResult> GetChildrenByParentId(string parentId)
    {
        var response = await diagnoseRequestRepository.GetChildrenByParentIdAsync(parentId);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("hospital/{hospitalId}")]
    public async Task<IActionResult> GetByHospitalId(string hospitalId)
    {
        var response = await diagnoseRequestRepository.GetByHospitalIdAsync(hospitalId);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDiagnoseRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await diagnoseRequestRepository.CreateAsync(dto);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("update-request-status")]
    public async Task<IActionResult> UpdateRequestStatus(string id, DiagnoseReqStatusEnum status)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await diagnoseRequestRepository.UpdateRequestStatus(id,  status);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/status/in-progress")]
    public async Task<IActionResult> SetStatusInProgress(string id)
    {
        var response = await diagnoseRequestRepository.SetStatusInProgress(id);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/status/not-possible")]
    public async Task<IActionResult> SetStatusNotPossible(string id)
    {
        var response = await diagnoseRequestRepository.SetStatusNotPossible(id);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/status/second-opinion-required")]
    public async Task<IActionResult> SetStatusSecondOpinionRequired(string id)
    {
        var response = await diagnoseRequestRepository.SetStatusSecondOpinionRequired(id);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/status/follow-up-scheduled")]
    public async Task<IActionResult> SetStatusFollowUpScheduled(string id)
    {
        var response = await diagnoseRequestRepository.SetStatusFollowUpScheduled(id);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/status/retake-imaging")]
    public async Task<IActionResult> SetStatusRetakeImaging(string id)
    {
        var response = await diagnoseRequestRepository.SetStatusRetakeImaging(id);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/status/post-imaging-review")]
    public async Task<IActionResult> SetStatusPostImagingReview(string id)
    {
        var response = await diagnoseRequestRepository.SetStatusPostImagingReview(id);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/status/completed")]
    public async Task<IActionResult> SetStatusCompleted(string id)
    {
        var response = await diagnoseRequestRepository.SetStatusCompleted(id);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/status/neglected")]
    public async Task<IActionResult> SetStatusNeglected(string id)
    {
        var response = await diagnoseRequestRepository.SetStatusNeglected(id);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/status/cancelled")]
    public async Task<IActionResult> SetStatusCancelled(string id)
    {
        var response = await diagnoseRequestRepository.SetStatusCancelled(id);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

}