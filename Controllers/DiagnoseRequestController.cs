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

}