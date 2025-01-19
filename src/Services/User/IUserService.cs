using IWantApp.DTOs.User;

namespace IWantApp.Services.User;

public interface IUserService
{
    Task<List<UserDTO>> GetAll();
    Task<UserDTO> GetById(int id);
    Task<UserDTO> GetByUsername(string username);
    Task Create(UserDTO dto);
    Task Update(UserDTO dto);
    Task Delete(int id);
}
