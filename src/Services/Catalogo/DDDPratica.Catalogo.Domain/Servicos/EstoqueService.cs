using DDDPratica.Catalogo.Domain.Eventos;
using DDDPratica.Catalogo.Domain.Produto;
using DDDPratica.Core.Communication.Mediator;
using DDDPratica.Core.EventBus;

namespace DDDPratica.Catalogo.Domain.Servicos;

public class EstoqueService : IEstoqueService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMediatorHandler _mediatorHandler; 

    public EstoqueService(
        IProdutoRepository produtoRepository,
        IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler; 
        _produtoRepository = produtoRepository;
    }

    public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if (produto == null) return false;
        
        if (!produto.PossuiEstoque(quantidade)) return false;
        
        produto.DebitarEstoque(quantidade);

        if (produto.QuantidadeEstoque < 10) 
            await _mediatorHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
        
        _produtoRepository.Atualizar(produto);

        return await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if (produto == null) return false;
        
        produto.ReporEstoque(quantidade);

        _produtoRepository.Atualizar(produto);

        return await _produtoRepository.UnitOfWork.Commit();
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
    }
}