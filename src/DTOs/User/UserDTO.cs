using IWantApp.DTOs.Base;
using IWantApp.DTOs.Role;
using System.ComponentModel.DataAnnotations;

namespace IWantApp.DTOs.User;

public class UserDTO : BaseDTO
{
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [MaxLength(50)]
    public string FullName { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }

    public ICollection<RoleDTO> Roles {  get; set; } = new List<RoleDTO>();
}

