﻿using DDDPratica.Core.Communication.Mediator;
using DDDPratica.Core.DomainObjects.Entidades;
using DDDPratica.Vendas.Infrastructure.Data.Context;

namespace DDDPratica.Vendas.Infrastructure;

public static class MediatorExtension
{
    public static async Task PublicarEventos(this IMediatorHandler mediator, VendasContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entidade>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList(); 
        
        domainEntities.ToList().ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents.Select(async (domainEvent) =>
        {
            await mediator.PublicarEvento(domainEvent);
        }); 

        await Task.WhenAll(tasks); 
    }
}