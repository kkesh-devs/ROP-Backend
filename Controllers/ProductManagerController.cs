using KKESH_ROP.DTO.ProductManager;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;


namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductManagerController(IProductManagerRepository productManagerRepository) : ControllerBase
{
    
//____________________________________________________________________________________________________________________________________________________
   
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await productManagerRepository.GetAllAsync();
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await productManagerRepository.GetByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductManagerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await productManagerRepository.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProductManagerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await productManagerRepository.UpdateAsync(id, dto);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

}