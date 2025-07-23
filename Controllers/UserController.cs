using KKESH_ROP.DTO.User;
using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace KKESH_ROP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository userRepository) : ControllerBase
{

//____________________________________________________________________________________________________________________________________________________

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await userRepository.GetAllAsync();
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await userRepository.GetByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await userRepository.CreateAsync(dto);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await userRepository.UpdateAsync(id, dto);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await userRepository.DeleteAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<string>(false, "Invalid model", null));

        var response = await userRepository.LoginAsync(dto);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
//____________________________________________________________________________________________________________________________________________________

}
