using KKESH_ROP.DTO.Physician;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhysicianController(IPhysicianRepository physicianRepository) : ControllerBase
{

//____________________________________________________________________________________________________________________________________________________

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await physicianRepository.GetAllAsync();
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await physicianRepository.GetByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var response = await physicianRepository.GetByUserIdAsync(userId);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePhysicianDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await physicianRepository.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePhysicianDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var updateResponse = await physicianRepository.UpdateAsync(id, dto);
        if (!updateResponse.Success) return NotFound(updateResponse);

        // Get the updated physician to return
        var physicianResponse = await physicianRepository.GetByIdAsync(id);
        if (!physicianResponse.Success) return NotFound(physicianResponse);

        return Ok(new Response<PhysicianDto>(true, "Physician updated successfully", physicianResponse.Data));
    }
//____________________________________________________________________________________________________________________________________________________

//     [HttpDelete("{id}")]
//     public async Task<IActionResult> Delete(string id)
//     {
//         var response = await physicianRepository.DeleteAsync(id);
//         if (!response.Success) return NotFound(response);
//         return Ok(response);
//     }
// //____________________________________________________________________________________________________________________________________________________

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(string status)
    {
        var response = await physicianRepository.GetByStatusAsync(status);
        return Ok(response);
    }
// //____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/approve")]
    public async Task<IActionResult> Approve(string id)
    {
        var response = await physicianRepository.ApproveAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
// //____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}/reject")]
    public async Task<IActionResult> Reject(string id)
    {
        var response = await physicianRepository.RejectAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

}