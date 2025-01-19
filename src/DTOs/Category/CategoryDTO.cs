using IWantApp.DTOs.Base;

namespace IWantApp.DTOs.Category;

public class CategoryDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;

    public CategoryDTO() { }

    public CategoryDTO(int id) 
    {
        Id = id;    
    }
}
