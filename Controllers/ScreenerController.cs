using KKESH_ROP.DTO.Screener;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScreenerController(IScreenerRepository screenerRepository) : ControllerBase
{

//____________________________________________________________________________________________________________________________________________________

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await screenerRepository.GetAllAsync();
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await screenerRepository.GetByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScreenerDto dto)
    {
        var response = await screenerRepository.CreateAsync(dto);
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateScreenerDto dto)
    {
        var response = await screenerRepository.UpdateAsync(id, dto);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateScreenerStatusDto dto)
    {
        var response = await screenerRepository.UpdateStatusAsync(id, dto.Status);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPatch("{id}/status/active")]
    public async Task<IActionResult> SetStatusActive(string id)
    {
        var response = await screenerRepository.UpdateStatusAsync(id, ScreenerStatusEnum.Active);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPatch("{id}/status/inactive")]
    public async Task<IActionResult> SetStatusInactive(string id)
    {
        var response = await screenerRepository.UpdateStatusAsync(id, ScreenerStatusEnum.Inactive);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(ScreenerStatusEnum status)
    {
        var response = await screenerRepository.GetByStatusAsync(status);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var response = await screenerRepository.GetByUserIdAsync(userId);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPatch("{id}/set-trained")]
    public async Task<IActionResult> SetAsTrained(string id)
    {
        var response = await screenerRepository.SetAsTrainedAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("hospital/{hospitalId}")]
    public async Task<IActionResult> GetByHospitalId(string hospitalId)
    {
        var response = await screenerRepository.GetByHospitalIdAsync(hospitalId);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

}