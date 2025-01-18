using IWantApp.DTOs.Product;
using IWantApp.Models;
using IWantApp.Services.Base;

public class ProductService : BaseService<Product, ProductDTO>, IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository, BaseConverter<Product, ProductDTO> converter)
        : base(productRepository, converter)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductDTO>> GetByName(string name)
    {
        var products = _productRepository.GetByName(name);
        return products.Select(p => _converter.ToDto(p)).ToList();
    }
}
