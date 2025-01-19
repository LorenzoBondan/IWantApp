using IWantApp.Config;
using IWantApp.Controllers.Exceptions;
using IWantApp.DTOs.Category;
using IWantApp.DTOs.Product;
using IWantApp.Models;
using IWantApp.Models.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);

// @Transactional
builder.Services.AddScoped<TransactionalAttribute>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration["Database:SqlServer"]));

builder.Services.AddScoped<BaseConverter<Category, CategoryDTO>, CategoryDtoToEntityAdapter>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<BaseConverter<Product, ProductDTO>, ProductDtoToEntityAdapter>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<CategoryDtoToEntityAdapter>();
builder.Services.AddScoped<ProductDtoToEntityAdapter>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();