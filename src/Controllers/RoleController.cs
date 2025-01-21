using IWantApp.DTOs.Role;
using IWantApp.Services.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
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

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var obj = await _service.GetById(id);
        return Ok(obj);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoleDTO dto)
    {
        await _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RoleDTO dto)
    {
        await _service.Update(dto);
        return Ok(dto);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _service.Delete(id);
        return NoContent(); 
    }
}
