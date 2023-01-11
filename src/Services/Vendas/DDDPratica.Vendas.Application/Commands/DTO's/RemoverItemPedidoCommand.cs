using DDDPratica.Core.Mensagens;
using FluentValidation;

namespace DDDPratica.Vendas.Application.Commands.DTO_s;

public class RemoverItemPedidoCommand : Command
{
    public Guid ClientId { get; set; }
    public Guid ProdutoId { get; set; }
    public Guid PedidoId { get; set; }

    protected RemoverItemPedidoCommand(Guid clientId, Guid produtoId, Guid pedidoId)
    {
        ClientId = clientId;
        ProdutoId = produtoId;
        PedidoId = pedidoId;
    }

    public override bool EhValido()
    {
        ValidationResult = new RemoverItemPedidoValidation().Validate(this);

        return ValidationResult.IsValid; 
    }
}

public class RemoverItemPedidoValidation : AbstractValidator<RemoverItemPedidoCommand>
{
    public RemoverItemPedidoValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");
        
        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");
        
        RuleFor(c => c.PedidoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do pedido inválido");
    }
}