using IWantApp.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace IWantApp.DTOs.Role;

public class RoleDTO : BaseDTO
{
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }

    [Required]
    [MaxLength(50)]
    public string Description { get; set; }

    public RoleDTO(int id)
    {
        Id = id;
    }

    public RoleDTO() { }
}
