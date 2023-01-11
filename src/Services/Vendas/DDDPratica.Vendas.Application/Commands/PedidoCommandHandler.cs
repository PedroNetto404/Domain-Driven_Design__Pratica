using DDDPratica.Core.Communication.Mediator;
using DDDPratica.Core.Mensagens;
using DDDPratica.Core.Mensagens.CommonMessages.DomainEvents;
using DDDPratica.Core.Mensagens.CommonMessages.Notifications;
using DDDPratica.Vendas.Application.Commands.DTO_s;
using DDDPratica.Vendas.Application.Events;
using DDDPratica.Vendas.Domain.Pedido.Entidades;
using DDDPratica.Vendas.Domain.Pedido.Repositories;
using MediatR;

namespace DDDPratica.Vendas.Application.Commands;

public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public PedidoCommandHandler(
        IPedidoRepository pedidoRepository,
        IMediatorHandler mediatorHandler)
    {
        _pedidoRepository = pedidoRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<bool> Handle(AdicionarItemPedidoCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return false;

        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClientId);

        var pedidoItem = new PedidoItem(request.ProdutoId, request.Nome, request.Quantidade, request.ValorUnitario);

        if (pedido == null)
        {
            pedido = Pedido.PedidoFactory.NovoPedidoRascunho(request.ClientId);
            pedido.AdicionarItem(pedidoItem);

            _pedidoRepository.Adicionar(pedido);
        }
        else
        {
            var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);
            pedido.AdicionarItem(pedidoItem);

            if (pedidoItemExistente)
            {
                _pedidoRepository.AtualizarItem(pedidoItem);
            }
            else
            {
                _pedidoRepository.AdicionarItem(pedidoItem);
            }

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClientId, pedido.Id, pedido.ValorTotal));
        }

        pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClientId, pedido.Id, request.ProdutoId, request.Nome, request.ValorUnitario, request.Quantidade));
        return await _pedidoRepository.UnitOfWork.Commit();
    }

    private bool ValidarComando(Command command)
    {
        if (command.EhValido()) return true;

        foreach (var error in command.ValidationResult.Errors)
        {
            _mediatorHandler.PublicarNotificacao
            (
                new DomainNotification
                (
                    command.MessageType,
                    error.ErrorMessage
                )
            );
        }

        return false;
    }
}