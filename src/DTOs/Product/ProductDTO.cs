using IWantApp.DTOs.Base;
using IWantApp.DTOs.Category;

namespace IWantApp.DTOs.Product;

public class ProductDTO : BaseDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryDTO CategoryDTO { get; set; }
}
