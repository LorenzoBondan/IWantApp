using IWantApp.Models;
using IWantApp.Repositories.Base;

public interface IProductRepository : IRepository<Product>
{
    public IQueryable<Product> FindAllWithDependencies();
    public Task<Product?> FindByIdWithDependencies(int id);
    List<Product> GetByName(string name);
}
