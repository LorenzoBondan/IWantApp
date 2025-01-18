using IWantApp.DTOs;
using IWantApp.DTOs.Product;
using IWantApp.Models;

public class ProductDtoToEntityAdapter : BaseConverter<Product, ProductDTO>
{
    private readonly CategoryEntityToDtoAdapter categoryEntityToDtoAdapter;

    public ProductDTO ToDto(Product entity)
    {
        return new ProductDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CategoryDTO = categoryEntityToDtoAdapter.ToDto(entity.Category)
        };
    }

    public Product ToEntity(ProductDTO dto)
    {
        return new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Category = categoryEntityToDtoAdapter.ToEntity(dto.CategoryDTO)
        };
    }
}
