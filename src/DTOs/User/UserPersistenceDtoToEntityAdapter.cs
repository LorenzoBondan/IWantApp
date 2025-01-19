using IWantApp.DTOs.User;
using IWantApp.Models;
using IWantApp.DTOs.Role;

public class UserPersistenceDtoToEntityAdapter
{
    public User ToEntity(UserPersistenceDTO dto)
    {
        return new User
        {
            Id = dto.Id,
            UserName = dto.UserName,
            PasswordHash = dto.Password,
            FullName = dto.FullName,
            Email = dto.Email,
            Address = dto.Address,
            NormalizedEmail = dto.Email.ToUpper(),
            NormalizedUserName = dto.UserName.ToUpper()
        };
    }

    public UserPersistenceDTO ToDto(User entity)
    {
        UserPersistenceDTO user = new UserPersistenceDTO()
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Password = entity.PasswordHash,
            FullName = entity.FullName,
            Email = entity.Email,
            Address = entity.Address,

            Roles = entity.UserRoles.Select(ur => new RoleDTO(ur.RoleId)).ToList()
        };

        return user;
    }
}
