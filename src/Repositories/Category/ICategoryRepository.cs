using IWantApp.Models;
using IWantApp.Repositories.Base;

public interface ICategoryRepository : IRepository<Category>
{
    List<Category> GetByName(string name);
}
