using IWantApp.DTOs.Role;

namespace IWantApp.Services.Role;

public interface IRoleService
{
    Task<List<RoleDTO>> GetAll();
    Task<RoleDTO> GetById(int id);
    Task Create(RoleDTO obj);
    Task Update(RoleDTO obj);
    Task Delete(int id);
}
