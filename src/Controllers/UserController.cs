using IWantApp.DTOs.User;
using IWantApp.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAll();
        return Ok(list);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetAllPaged([FromQuery] int page = 0, [FromQuery] int size = 10, [FromQuery] string? sortedBy = null)
    {
        var pagedResponse = await _service.GetAllPaged(page, size, sortedBy);
        return Ok(pagedResponse);
    }

    [Authorize(Roles = "Admin, Client")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var obj = await _service.GetById(id);
        return Ok(obj);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserPersistenceDTO dto)
    {
        await _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [Authorize(Roles = "Admin, Client")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserPersistenceDTO dto)
    {
        await _service.Update(dto);
        return Ok(dto);
    }

    [Authorize(Roles = "Admin, Client")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}
