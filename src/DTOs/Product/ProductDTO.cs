using IWantApp.DTOs.Base;
using IWantApp.DTOs.Category;
using System.ComponentModel.DataAnnotations;

namespace IWantApp.DTOs.Product;

public class ProductDTO : BaseDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
    public string? Name { get; set; }
    [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "A categoria é obrigatória.")]
    public CategoryDTO? Category { get; set; }

    public ProductDTO() { }

    public ProductDTO(int id)
    {
        Id = id;
    }
}
