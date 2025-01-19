using IWantApp.DTOs.Product;
using IWantApp.Models;

public class ProductDtoToEntityAdapter : BaseConverter<Product, ProductDTO>
{
    private readonly CategoryDtoToEntityAdapter categoryEntityToDtoAdapter;

    public ProductDtoToEntityAdapter(CategoryDtoToEntityAdapter categoryEntityToDtoAdapter)
    {
        this.categoryEntityToDtoAdapter = categoryEntityToDtoAdapter;
    }

    public Product ToEntity(ProductDTO dto)
    {
        return new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            CategoryId = dto.Category.Id
        };
    }

    public ProductDTO ToDto(Product entity)
    {
        return new ProductDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Category = entity.Category != null
                ? categoryEntityToDtoAdapter.ToDto(entity.Category)
                : null
        };
    }
}
