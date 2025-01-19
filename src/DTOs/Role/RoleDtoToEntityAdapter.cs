using IWantApp.DTOs.Role;
using IWantApp.Models;

public class RoleDtoToEntityAdapter
{
    public Role ToEntity(RoleDTO dto)
    {
        return new Role
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description
        };
    }

    public RoleDTO ToDto(Role entity)
    {
        return new RoleDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        };
    }
}
