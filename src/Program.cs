using IWantApp.Config;
using IWantApp.Controllers.Exceptions;
using IWantApp.DTOs.Category;
using IWantApp.DTOs.Product;
using IWantApp.Models;
using IWantApp.Models.Context;
using IWantApp.Services.Auth;
using IWantApp.Services.Role;
using IWantApp.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados SQL Server
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configura��o do JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// @Transactional
builder.Services.AddScoped<TransactionalAttribute>();

// Servi�os e Reposit�rios
builder.Services.AddScoped<BaseConverter<Category, CategoryDTO>, CategoryDtoToEntityAdapter>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<BaseConverter<Product, ProductDTO>, ProductDtoToEntityAdapter>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<CategoryDtoToEntityAdapter>();
builder.Services.AddScoped<ProductDtoToEntityAdapter>();
builder.Services.AddScoped<RoleDtoToEntityAdapter>();
builder.Services.AddScoped<UserDtoToEntityAdapter>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<CustomUserUtil>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Servi�os para APIs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionando filtros globais para exce��es
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

var app = builder.Build();

// Configura��o de desenvolvimento (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware de autentica��o e autoriza��o
app.UseAuthentication(); // Habilita autentica��o
app.UseAuthorization(); // Habilita autoriza��o

// Mapeia os controladores
app.MapControllers();

// Roda a aplica��o
app.Run();
