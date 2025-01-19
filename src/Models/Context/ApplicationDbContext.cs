namespace IWantApp.Models.Context;

using IWantApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
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
        base.OnModelCreating(builder);

        // Configuração do relacionamento UserRole
        builder.Entity<IdentityUserRole<int>>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId }); // Chave primária composta

            // Relacionamento User -> UserRole
            userRole.HasOne<User>()
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            // Relacionamento Role -> UserRole
            userRole.HasOne<Role>()
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });

        // Relacionamentos
        builder.Entity<Product>()
        .HasOne(p => p.Category) // Produto tem uma Categoria
        .WithMany(c => c.Products) // Categoria pode ter muitos Produtos
        .HasForeignKey(p => p.CategoryId) // Chave estrangeira no Produto
        .OnDelete(DeleteBehavior.Restrict); // Comportamento de exclusão
    }
}