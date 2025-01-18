using IWantApp.Models;
using IWantApp.Repositories.Base;

public interface IProductRepository : IRepository<Product>
{
    List<Product> GetByName(string name);
}
