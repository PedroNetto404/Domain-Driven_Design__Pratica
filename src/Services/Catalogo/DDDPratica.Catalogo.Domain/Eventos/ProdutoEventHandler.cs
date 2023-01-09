using DDDPratica.Catalogo.Domain.Produto;
using MediatR;

namespace DDDPratica.Catalogo.Domain.Eventos;

public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoEventHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public async Task Handle(ProdutoAbaixoEstoqueEvent notification, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterPorId(notification.AggregateId); 
        
        //Enviar email para a aquisição de mais produtos;
    }
}