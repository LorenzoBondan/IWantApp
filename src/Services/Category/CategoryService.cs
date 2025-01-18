using IWantApp.DTOs.Category;
using IWantApp.Models;
using IWantApp.Services.Base;

public class CategoryService : BaseService<Category, CategoryDTO>, ICategoryService
{
    private readonly ICategoryRepository _CategoryRepository;

    public CategoryService(ICategoryRepository CategoryRepository, BaseConverter<Category, CategoryDTO> converter)
        : base(CategoryRepository, converter)
    {
        _CategoryRepository = CategoryRepository;
    }

    public async Task<List<CategoryDTO>> GetByName(string name)
    {
        var Categorys = _CategoryRepository.GetByName(name);
        return Categorys.Select(p => _converter.ToDto(p)).ToList();
    }
}