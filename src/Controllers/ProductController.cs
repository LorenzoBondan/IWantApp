using IWantApp.Config;
using IWantApp.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService service;

    public ProductController(IProductService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(service.FindAllWithCategory());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetById([FromRoute] int id)
    {
        var dto = await service.FindByIdWithCategory(id);
        return dto != null ? Ok(dto) : NotFound();
    }

    [ServiceFilter(typeof(TransactionalAttribute))]
    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductDTO dto)
    {
        var createdObj = await service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdObj.Id }, createdObj);
    }

    [ServiceFilter(typeof(TransactionalAttribute))]
    [HttpPut]
    public async Task<ActionResult<ProductDTO>> Update([FromBody] ProductDTO dto)
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
