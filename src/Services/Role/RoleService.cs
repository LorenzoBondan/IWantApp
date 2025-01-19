using IWantApp.DTOs.Role;
using IWantApp.Services.Exceptions;

namespace IWantApp.Services.Role;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly RoleDtoToEntityAdapter _roleDtoToEntityAdapter;

    public RoleService(IRoleRepository roleRepository, RoleDtoToEntityAdapter roleDtoToEntityAdapter)
    {
        _roleRepository = roleRepository;
        _roleDtoToEntityAdapter = roleDtoToEntityAdapter;
    }

    public async Task<List<RoleDTO>> GetAll()
    {
        var roles = await _roleRepository.GetAll();
        return roles.Select(r => _roleDtoToEntityAdapter.ToDto(r)).ToList();
    }

    public async Task<RoleDTO> GetById(int id)
    {
        var role = await _roleRepository.GetById(id);
        if (role == null)
        {
            throw new ResourceNotFoundException("Role não encontrada com id: " + id);
        }
        return _roleDtoToEntityAdapter.ToDto(role);
    }

    public async Task Create(RoleDTO obj)
    {
        var role = _roleDtoToEntityAdapter.ToEntity(obj);
        await _roleRepository.Create(role);
    }

    public async Task Update(RoleDTO obj)
    {
        var role = _roleDtoToEntityAdapter.ToEntity(obj);
        await _roleRepository.Update(role);
    }

    public async Task Delete(int id)
    {
        await _roleRepository.Delete(id);
    }
}
