using IWantApp.DTOs.Product;
using IWantApp.Models;
using IWantApp.Services.Base;

public interface IProductService : IBaseService<Product, ProductDTO>
{
    Task<List<ProductDTO>> GetByName(string name);
}
