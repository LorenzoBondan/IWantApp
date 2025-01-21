using IWantApp.DTOs.Product;
using IWantApp.Models;
using IWantApp.Services.Base;
using IWantApp.Utils;
using System.Linq.Expressions;

public interface IProductService : IBaseService<Product, ProductDTO>
{
    IQueryable<ProductDTO> FindAllWithCategory();
    Task<ProductDTO?> FindByIdWithCategory(int id);
    Task<PageableResponse<ProductDTO>> GetPagedProductsWithCategory(
    int page,
    int size,
    string? sortedBy = null,
    Expression<Func<Product, bool>>? filter = null);
    Task<List<ProductDTO>> GetByName(string name);
}
