using IWantApp.DTOs.Product;
using IWantApp.Models;
using IWantApp.Services.Base;

public interface IProductService : IBaseService<Product, ProductDTO>
{
    IQueryable<ProductDTO> FindAllWithCategory();
    Task<ProductDTO?> FindByIdWithCategory(int id);
    Task<List<ProductDTO>> GetByName(string name);
}
