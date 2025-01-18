using IWantApp.DTOs.Category;
using IWantApp.Models;
using IWantApp.Services.Base;

public interface ICategoryService : IBaseService<Category, CategoryDTO>
{
    Task<List<CategoryDTO>> GetByName(string name);
}
