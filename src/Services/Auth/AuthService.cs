using IWantApp.Utils;

namespace IWantApp.Services.Auth;

public class AuthService : IAuthService
{
    private readonly CustomUserUtil _customUserUtil;

    public AuthService(CustomUserUtil customUserUtil)
    {
        _customUserUtil = customUserUtil;
    }

    public string GetLoggedUsername()
    {
        return _customUserUtil.GetLoggedUsername();
    }

    public bool isAdmin()
    {
        var roles = _customUserUtil.GetLoggedUserRoles();
        return roles.Contains("Admin");
    }
}
