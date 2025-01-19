using IWantApp.DTOs.User;
using IWantApp.Models;
using IWantApp.DTOs.Role;

public class UserDtoToEntityAdapter
{
    public User ToEntity(UserDTO dto)
    {
        return new User
        {
            Id = dto.Id,
            FullName = dto.FullName,
            Email = dto.Email,
            Address = dto.Address,
        };
    }

    public UserDTO ToDto(User entity)
    {
        UserDTO user = new UserDTO()
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Email = entity.Email,
            Address = entity.Address,

            Roles = entity.UserRoles.Select(ur => new RoleDTO(ur.RoleId)).ToList()
        };

        return user;
    }
}
