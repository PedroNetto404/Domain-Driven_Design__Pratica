using System.Collections;
using AutoMapper;
using DDDPratica.Catalogo.Application.DTO_s;
using DDDPratica.Catalogo.Domain.Produto;
using DDDPratica.Catalogo.Domain.Produto.Entidades;
using DDDPratica.Catalogo.Domain.Servicos;
using DDDPratica.Core.DomainObjects;
using DDDPratica.Core.DomainObjects.Exceptions;

namespace DDDPratica.Catalogo.Application.Serviços;

public class ProdutoAppService : IProdutoAppService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IEstoqueService _estoqueService;
    private readonly IMapper _mapper;

    public ProdutoAppService
    (
        IProdutoRepository produtoRepository,
        IEstoqueService estoqueService,
        IMapper mapper
    )
    {
        _produtoRepository = produtoRepository;
        _estoqueService = estoqueService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo)
    {
        return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterPorCategoria(codigo));
    }

    public async Task<ProdutoDto> ObterPorId(Guid id)
    {
        return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id)); 
    }

    public async Task<IEnumerable<ProdutoDto>> ObterTodos()
    {
        return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterTodos());
    }

    public async Task<IEnumerable<CategoriaDto>> ObterCategorias()
    {
        return _mapper.Map<IEnumerable<CategoriaDto>>(await _produtoRepository.ObterCategorias());
    }

    public async Task AdicionarProduto(ProdutoDto produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto); 

        _produtoRepository.Adicionar(produto);

        await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task AtualizarProduto(ProdutoDto produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto); 
        
        _produtoRepository.Atualizar(produto);

        await _produtoRepository.UnitOfWork.Commit(); 
    }

    public async Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade)
    {
        if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
            throw new DomainException("Falha ao debitar o estoque"); 

        return _mapper.Map<ProdutoDto>(_produtoRepository.ObterPorId(id));
    }

    public async Task<ProdutoDto> ReporEstoque(Guid id, int quantidade)
    {
        if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
            throw new DomainException("Falha ao repor o estoque"); 

        return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id)); 
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
        _estoqueService?.Dispose();
    }
}