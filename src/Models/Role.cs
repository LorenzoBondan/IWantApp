namespace IWantApp.Models;

using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole<int> 
{
    public string Description { get; set; }

    public ICollection<IdentityUserRole<int>> UserRoles { get; set; } = new List<IdentityUserRole<int>>();
}

