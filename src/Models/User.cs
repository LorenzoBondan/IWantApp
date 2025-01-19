namespace IWantApp.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public string FullName { get; set; }
    public string Address { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
}

