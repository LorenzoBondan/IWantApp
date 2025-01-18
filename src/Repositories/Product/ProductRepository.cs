using IWantApp.Infra.Data;
using IWantApp.Models;
using IWantApp.Repositories.Base;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public List<Product> GetByName(string name)
    {
        return _context.Products.Where(p => p.Name == name).ToList();
    }
}
