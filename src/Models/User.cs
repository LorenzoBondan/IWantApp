namespace IWantApp.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public string FullName { get; set; }
    public string Address { get; set; }

    public ICollection<IdentityUserRole<int>> UserRoles { get; set; } = new List<IdentityUserRole<int>>();
}

