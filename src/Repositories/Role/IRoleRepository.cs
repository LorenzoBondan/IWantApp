using IWantApp.Models;
using IWantApp.Utils;

public interface IRoleRepository
{
    Task<List<Role>> GetAll();
    Task<Role> GetById(int id);
    Task<PageableResponse<Role>> GetAllPaged(int page, int size, string? sortedBy = null);
    Task Create(Role obj);
    Task Update(Role obj);
    Task Delete(int id);
}
