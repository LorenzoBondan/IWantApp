using System.Security.Claims;

namespace IWantApp.Utils;

public class CustomUserUtil
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomUserUtil(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetLoggedUsername()
    {
        var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        return username ?? string.Empty;
    }

    public List<string> GetLoggedUserRoles()
    {
        return _httpContextAccessor.HttpContext?.User?
            .Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList() ?? new List<string>();
    }
}
