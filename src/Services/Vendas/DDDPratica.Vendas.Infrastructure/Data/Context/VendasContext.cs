using DDDPratica.Core.Communication.Mediator;
using DDDPratica.Core.Data;
using DDDPratica.Vendas.Domain.Pedido.Entidades;
using Microsoft.EntityFrameworkCore;

namespace DDDPratica.Vendas.Infrastructure.Data.Context;

public class VendasContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediator; 
    public DbSet<Pedido> Pedido { get; set; }
    public DbSet<PedidoItem> PedidoItems { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }

    public VendasContext(DbContextOptions<VendasContext> options, IMediatorHandler mediator) : base(options)
    {
        _mediator = mediator;
    }
    
    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added) entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            if (entry.State == EntityState.Modified) entry.Property("DataCadastro").IsModified = false; 
        }

        if (await SaveChangesAsync() > 0)
        {
            await _mediator.PublicarEventos(this);
            return true; 
        }

        return false; 
    }
}