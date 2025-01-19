using IWantApp.Config;
using IWantApp.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService service;

    public CategoryController(ICategoryService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(service.FindAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetById([FromRoute] int id)
    {
        var dto = await service.FindById(id, null);
        return dto != null ? Ok(dto) : NotFound(); 
    }

    [ServiceFilter(typeof(TransactionalAttribute))]
    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> Create([FromBody] CategoryDTO dto)
    {
        var createdObj = await service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdObj.Id }, createdObj);
    }

    [ServiceFilter(typeof(TransactionalAttribute))]
    [HttpPut]
    public async Task<ActionResult<CategoryDTO>> Update([FromBody] CategoryDTO dto)
    {
        var updatedObj = await service.Update(dto);
        return updatedObj != null ? Ok(updatedObj) : NotFound();
    }

    [ServiceFilter(typeof(TransactionalAttribute))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await service.Delete(id);
        return NoContent();
    }
}
