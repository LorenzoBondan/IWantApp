namespace IWantApp.Infra.Data;

using IWantApp.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    // conexão
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // mapeamento -> nomes das tabelas
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        // todo atributo string tem no máximo 50 caracteres
        configuration.Properties<string>()
            .HaveMaxLength(50);
        base.ConfigureConventions(configuration);
    }

    // modelagem da criação de classes no Migration
    // personalizadas, caso algum atributo não siga a ConfigureConventions
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // atributos do Produto
        builder.Entity<Product>()
            .Property(p => p.Name).IsRequired();
        builder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(200).IsRequired(false);
        builder.Entity<Category>()
            .Property(p => p.Name).IsRequired();
    }
}
