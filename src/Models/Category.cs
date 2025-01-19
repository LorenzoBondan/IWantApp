using IWantApp.Models.Base;

namespace IWantApp.Models;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public Category() { }

    public Category(int id)
    {
        Id = id;
    }
}
