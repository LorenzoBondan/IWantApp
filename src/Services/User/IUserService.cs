using IWantApp.DTOs.User;
using IWantApp.Utils;

namespace IWantApp.Services.User;

public interface IUserService
{
    Task<List<UserDTO>> GetAll();
    Task<PageableResponse<UserDTO>> GetAllPaged(int page, int size, string? sortedBy = null);
    Task<UserDTO> GetById(int id);
    UserDTO GetByUsername(string username);
    Task Create(UserPersistenceDTO dto);
    Task Update(UserPersistenceDTO dto);
    Task Delete(int id);
}
