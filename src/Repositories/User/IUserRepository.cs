using IWantApp.Models;
using IWantApp.Utils;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<PageableResponse<User>> GetAllPaged(int page, int size, string? sortedBy = null);
    Task<User> GetById(int id);
    User GetByUsername(string username);
    Task Create(User obj);
    Task Update(User obj);
    Task Delete(int id);
}
