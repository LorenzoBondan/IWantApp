using IWantApp.DTOs.Category;
using IWantApp.Models;

public class CategoryDtoToEntityAdapter : BaseConverter<Category, CategoryDTO>
{
    public CategoryDTO ToDto(Category entity)
    {
        return new CategoryDTO
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public Category ToEntity(CategoryDTO dto)
    {
        return new Category
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }
}
