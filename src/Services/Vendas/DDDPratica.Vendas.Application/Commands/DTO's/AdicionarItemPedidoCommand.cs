using DDDPratica.Core.Mensagens;
using FluentValidation;

namespace DDDPratica.Vendas.Application.Commands.DTO_s;

public class AdicionarItemPedidoCommand : Command
{
    public Guid ClientId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string Nome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    public AdicionarItemPedidoCommand
    (
        Guid clientId,
        Guid produtoId,
        string nome,
        int quantidade,
        decimal valorUnitario
    )
    {
        ClientId = clientId;
        ProdutoId = produtoId;
        Nome = nome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public override bool EhValido()
    {
        ValidationResult = new AdicionarItemPedidoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
{
    public AdicionarItemPedidoValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Produto Id não pode ser vazio");

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("O nome do produto não pode ser vazio");

        RuleFor(c => c.Quantidade)
            .GreaterThan(0)
            .WithMessage("A quantidade minima de um item é 1")
            .LessThan(15)
            .WithMessage("A quantidade máxima de um item é 15");

        RuleFor(c => c.ValorUnitario)
            .GreaterThan(0)
            .WithMessage("O valor do item precisa ser maior que 0");
    }
}