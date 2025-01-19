namespace IWantApp.Models.Context;

using IWantApp.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    // conexão
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // mapeamento -> nomes das tabelas
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    // modelagem da criação de classes no Migration
    // personalizadas, caso algum atributo não siga a ConfigureConventions
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Relacionamentos
        builder.Entity<Product>()
        .HasOne(p => p.Category) // Produto tem uma Categoria
        .WithMany(c => c.Products) // Categoria pode ter muitos Produtos
        .HasForeignKey(p => p.CategoryId) // Chave estrangeira no Produto
        .OnDelete(DeleteBehavior.Restrict); // Comportamento de exclusão
    }
}
