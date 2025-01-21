﻿using IWantApp.Config;
using IWantApp.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Admin, Client")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(service.FindAllWithCategory());
    }

    [Authorize(Roles = "Admin, Client")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetAllPaged([FromQuery] int page = 0, [FromQuery] int size = 10, [FromQuery] string? sortedBy = null)
    {
        var pagedResponse = await service.GetPagedProductsWithCategory(page, size, sortedBy);
        return Ok(pagedResponse);
    }

    [Authorize(Roles = "Admin, Client")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetById([FromRoute] int id)
    {
        var dto = await service.FindByIdWithCategory(id);
        return dto != null ? Ok(dto) : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(TransactionalAttribute))]
    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductDTO dto)
    {
        var createdObj = await service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdObj.Id }, createdObj);
    }

    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(TransactionalAttribute))]
    [HttpPut]
    public async Task<ActionResult<ProductDTO>> Update([FromBody] ProductDTO dto)
    {
        var updatedObj = await service.Update(dto);
        return updatedObj != null ? Ok(updatedObj) : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(TransactionalAttribute))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await service.Delete(id);
        return NoContent();
    }
}
