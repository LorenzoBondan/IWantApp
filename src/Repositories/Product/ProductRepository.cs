using IWantApp.Models;
using IWantApp.Models.Context;
using IWantApp.Repositories.Base;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Product> FindAllWithDependencies()
    {
        return FindAll(p => p.Category);
    }

    public async Task<Product?> FindByIdWithDependencies(int id)
    {
        return await FindById(id, p => p.Category);
    }

    public List<Product> GetByName(string name)
    {
        return _context.Products.Where(p => p.Name == name).ToList();
    }
}
