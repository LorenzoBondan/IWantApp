using IWantApp.DTOs.Product;
using IWantApp.Models;
using IWantApp.Services.Base;
using Microsoft.EntityFrameworkCore;

public class ProductService : BaseService<Product, ProductDTO>, IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository, BaseConverter<Product, ProductDTO> converter)
        : base(productRepository, converter)
    {
        _productRepository = productRepository;
    }

    public IQueryable<ProductDTO> FindAllWithCategory()
    {
        var products = _repository.FindAll().Include(p => p.Category);
        return products.Select(p => _converter.ToDto(p));
    }

    public async Task<ProductDTO?> FindByIdWithCategory(int id)
    {
        var product = await _repository.FindById(id, p => p.Category);
        return product == null ? null : _converter.ToDto(product);
    }

    public async Task<List<ProductDTO>> GetByName(string name)
    {
        var products = _productRepository.GetByName(name);
        return products.Select(p => _converter.ToDto(p)).ToList();
    }
}
