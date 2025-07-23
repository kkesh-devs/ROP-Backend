using KKESH_ROP.DTO.TapPayment;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TapPaymentController(ITapPaymentRepository tapPaymentRepository) :  ControllerBase
{
    
//____________________________________________________________________________________________________________________________________________________

    [HttpPost]
    public async Task<IActionResult> Charge([FromBody] ChargeReqDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));
        
        var response = await tapPaymentRepository.Charge(dto);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("{chargeId}")]
    public async Task<IActionResult> RetrieveCharge(string chargeId)
    {
        var response = await tapPaymentRepository.RetrieveCharge(chargeId);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________
}