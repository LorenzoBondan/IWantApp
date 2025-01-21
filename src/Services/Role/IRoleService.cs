using IWantApp.DTOs.Role;
using IWantApp.Utils;

namespace IWantApp.Services.Role;

public interface IRoleService
{
    Task<List<RoleDTO>> GetAll();
    Task<PageableResponse<RoleDTO>> GetAllPaged(int page, int size, string? sortedBy = null);
    Task<RoleDTO> GetById(int id);
    Task Create(RoleDTO dto);
    Task Update(RoleDTO dto);
    Task Delete(int id);
}
