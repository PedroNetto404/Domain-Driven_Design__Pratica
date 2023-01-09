using DDDPratica.Catalogo.Domain.Produto;
using DDDPratica.Catalogo.Domain.Produto.Entidades;
using DDDPratica.Catalogo.Infrastructure.Data.Context;
using DDDPratica.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DDDPratica.Catalogo.Infrastructure.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly CatalogoContext _context;
    
    public IUnitOfWork UnitOfWork => _context;

    public ProdutoRepository(CatalogoContext context)
    {
        _context = context; 
    }
    public async Task<IEnumerable<Produto>> ObterTodos()
    {
        return await _context
            .Produtos
            .AsNoTracking()
            .ToListAsync(); 
    }

    public async Task<Produto> ObterPorId(Guid id)
    {
        return await _context
            .Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id); 
    }

    public async Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
    {
        return await _context
            .Produtos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .Where(p => p.Categoria.Codigo == codigo)
            .ToListAsync(); 
    }

    public async Task<IEnumerable<Categoria>> ObterCategorias()
    {
        return await _context.Categorias.ToListAsync(); 
    }

    public void Adicionar(Produto produto)
    {
        _context.Produtos.Add(produto); 
    }

    public void Atualizar(Produto produto)
    {
        _context.Produtos.Update(produto); 
    }

    public void Adicionar(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
    }

    public void Atualizar(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
    }
    public void Dispose()
    {
        _context?.Dispose();
    }
}