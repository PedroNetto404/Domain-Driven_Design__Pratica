using DDDPratica.Catalogo.Domain.Produto.Entidades;
using DDDPratica.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DDDPratica.Catalogo.Infrastructure.Data.Context;

public class CatalogoContext : DbContext, IUnitOfWork
{
    public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; }
    public DbSet<Categoria> Categorias { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in 
                 modelBuilder.Model
                     .GetEntityTypes()
                     .SelectMany
                         (
                             e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(100)");
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added) entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            if (entry.State == EntityState.Modified) entry.Property("DataCadastro").IsModified = false; 
        }

        return await SaveChangesAsync() > 0;
    }
}