using IWantApp.Models;

public interface IRoleRepository
{
    Task<List<Role>> GetAll();
    Task<Role> GetById(int id);
    Task Create(Role obj);
    Task Update(Role obj);
    Task Delete(int id);
}
