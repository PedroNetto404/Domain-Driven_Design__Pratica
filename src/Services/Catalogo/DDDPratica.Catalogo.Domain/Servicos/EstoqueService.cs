using DDDPratica.Catalogo.Domain.Events;
using DDDPratica.Catalogo.Domain.Produto;
using DDDPratica.Catalogo.Domain.Servicos;
using DDDPratica.Core;
using DDDPratica.Core.EventBus;

namespace DDDPratica.Catalogo.Domain.Services;

public class EstoqueService : IEstoqueService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IEventHandler _eventBus; 

    public EstoqueService(
        IProdutoRepository produtoRepository,
        IEventHandler eventHandler)
    {
        _eventBus = eventHandler; 
        _produtoRepository = produtoRepository;
    }

    public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if (produto == null) return false;
        
        if (!produto.PossuiEstoque(quantidade)) return false;
        
        produto.DebitarEstoque(quantidade);

        if (produto.QuantidadeEstoque < 10) 
            await _eventBus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
        
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