using KKESH_ROP.DTO.MedicalCenter;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicalCenterController(IMedicalCenterRepository medicalCenterRepository) : ControllerBase
{
//____________________________________________________________________________________________________________________________________________________

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await medicalCenterRepository.GetAllAsync();
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await medicalCenterRepository.GetByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response); 
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var response = await medicalCenterRepository.GetByUserIdAsync(userId);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicalCenterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await medicalCenterRepository.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateMedicalCenterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await medicalCenterRepository.UpdateAsync(id, dto);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] MedicalCenterStatus status)
    {
        var response = await medicalCenterRepository.UpdateStatusAsync(id, status);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPatch("{id}/approve")]
    public async Task<IActionResult> Approve(string id)
    {
        var response = await medicalCenterRepository.ApproveMedicalCenterAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpPatch("{id}/set-integration")]
    public async Task<IActionResult> SetIntegration(string id)
    {
        var response = await medicalCenterRepository.SetIntegrationStatusAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpPatch("{id}/set-validation")]
    public async Task<IActionResult> SetValidation(string id)
    {
        var response = await medicalCenterRepository.SetValidationStatusAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpPatch("{id}/activate")]
    public async Task<IActionResult> Activate(string id)
    {
        var response = await medicalCenterRepository.ActivateMedicalCenterAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpPatch("{id}/set-pending")]
    public async Task<IActionResult> SetPending(string id)
    {
        var response = await medicalCenterRepository.SetPendingStatusAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
}
