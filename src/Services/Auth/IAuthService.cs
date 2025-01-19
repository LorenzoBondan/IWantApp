namespace IWantApp.Services.Auth;

public interface IAuthService
{
    string GetLoggedUsername();
    bool isAdmin();
}
