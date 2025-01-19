using IWantApp.DTOs.User;

namespace IWantApp.Services.User;

public interface IUserService
{
    Task<List<UserDTO>> GetAll();
    Task<UserDTO> GetById(int id);
    UserDTO GetByUsername(string username);
    Task Create(UserPersistenceDTO dto);
    Task Update(UserPersistenceDTO dto);
    Task Delete(int id);
}
