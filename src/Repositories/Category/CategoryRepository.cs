using IWantApp.Models;
using IWantApp.Models.Context;
using IWantApp.Repositories.Base;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public List<Category> GetByName(string name)
    {
        return _context.Categories.Where(c => c.Name == name).ToList();
    }
}
