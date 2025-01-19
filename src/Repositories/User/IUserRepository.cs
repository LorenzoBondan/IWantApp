using IWantApp.Models;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User> GetById(int id);
    Task Create(User obj);
    Task Update(User obj);
    Task Delete(int id);
}
