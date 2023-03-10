using DDDPratica.Catalogo.Domain.Produto.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDPratica.Catalogo.Infrastructure.Data.Mappings;

public class CategoriaMap : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder
            .Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(250)");

        // 1 : N => Categoria : Produtos
        builder
            .HasMany(c => c.Produtos)
            .WithOne(p => p.Categoria)
            .HasForeignKey(p => p.CategoriaId);
        
        builder.ToTable("Categorias");
    }
}