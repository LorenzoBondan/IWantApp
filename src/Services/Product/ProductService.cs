using IWantApp.DTOs.Product;
using IWantApp.Models;
using IWantApp.Services.Base;
using IWantApp.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<PageableResponse<ProductDTO>> GetPagedProductsWithCategory(
    int page,
    int size,
    string? sortedBy = null,
    Expression<Func<Product, bool>>? filter = null)
    {
        var pagedResult = await _repository.GetAllPaged(
            page,
            size,
            sortedBy,
            filter,
            p => p.Category
        );

        var content = pagedResult.Content.Select(p => _converter.ToDto(p)).ToList();

        return new PageableResponse<ProductDTO>
        {
            NumberOfElements = pagedResult.NumberOfElements,
            Page = pagedResult.Page,
            TotalPages = pagedResult.TotalPages,
            Size = pagedResult.Size,
            First = pagedResult.First,
            Last = pagedResult.Last,
            SortedBy = pagedResult.SortedBy,
            Content = content
        };
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
