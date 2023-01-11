using System.Data;
using DDDPratica.Core.Mensagens;
using FluentValidation;

namespace DDDPratica.Vendas.Application.Commands.DTO_s;

public class AtualizarItemPedidoCommand : Command
{
    public Guid ClientId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public Guid PedidoId { get; private set; }
    public int Quantidade { get; private set; }

    protected AtualizarItemPedidoCommand(Guid clientId, Guid produtoId, Guid pedidoId, int quantidade)
    {
        ClientId = clientId;
        ProdutoId = produtoId;
        PedidoId = pedidoId;
        Quantidade = quantidade;
    }
}

public class AtualizarItemPedidoValidation : AbstractValidator<AtualizarItemPedidoCommand>
{
    public AtualizarItemPedidoValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");
        
        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");

        RuleFor(c => c.Quantidade)
            .GreaterThan(0)
            .WithMessage("A quantidade minima de um item é 1")
            .LessThan(15)
            .WithMessage("A quantidade minima de um item é 15");
    }
}