using IWantApp.DTOs.User;

namespace IWantApp.Services.Auth;

public interface IAuthService
{
    UserDTO Authenticated();
    void ValidateSelfOrAdmin(int userId);
}
