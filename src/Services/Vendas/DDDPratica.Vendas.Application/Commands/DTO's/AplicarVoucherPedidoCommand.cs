using DDDPratica.Core.Mensagens;
using FluentValidation;

namespace DDDPratica.Vendas.Application.Commands.DTO_s;

public class AplicarVoucherPedidoCommand : Command
{
    public Guid ClienteId { get; set; }
    public Guid PedidoId { get; set; }
    public string CodigoVoucher { get; set; }

    protected AplicarVoucherPedidoCommand(Guid clienteId, Guid pedidoId, string codigoVoucher)
    {
        ClienteId = clienteId;
        PedidoId = pedidoId;
        CodigoVoucher = codigoVoucher;
    }

    public override bool EhValido()
    {
        ValidationResult = new AplicarVoucherPedidoValidation().Validate(this);

        return ValidationResult.IsValid; 
    }
}public class AplicarVoucherPedidoValidation : AbstractValidator<AplicarVoucherPedidoCommand>
{
    public AplicarVoucherPedidoValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");
        
        RuleFor(c => c.PedidoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do pedido inválido"); 
        
        RuleFor(c => c.CodigoVoucher)
            .NotEmpty()
            .WithMessage("O código do voucher não pode ser vazio");
    }
}