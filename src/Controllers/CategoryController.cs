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
    public async Task<ActionResult<List<CategoryDTO>>> GetAll()
    {
        //var categories = await service.FindAll();
        //return Ok(categories);
        return null;
    }

    [HttpGet("teste")]
    public ActionResult<string> Teste()
    {
        try
        {
            string a = "teste";
            return Ok(a);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetById([FromRoute] int id)
    {
        var category = await service.FindById(id);
        return category != null ? Ok(category) : NotFound(); 
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> Create([FromBody] CategoryDTO dto)
    {
        var createdObj = await service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdObj.Id }, createdObj);
    }

    [HttpPut]
    public async Task<ActionResult<CategoryDTO>> Update([FromBody] CategoryDTO dto)
    {
        var updatedCategory = await service.Update(dto);
        return updatedCategory != null ? Ok(updatedCategory) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await service.Delete(id);
        return NoContent();
    }
}
