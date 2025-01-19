using IWantApp.Models.Base;

namespace IWantApp.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }

    public Product() { }

    public Product(int id)
    {
        Id = id;
    }
}
