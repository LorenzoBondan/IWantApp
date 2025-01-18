using IWantApp.DTOs.Category;

namespace IWantApp.DTOs.Product;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryDTO CategoryDTO { get; set; }
}
