using DDDPratica.Catalogo.Domain.Produto.Entidades;
using DDDPratica.Core.Data;

namespace DDDPratica.Catalogo.Domain.Produto;

public interface IProdutoRepository : IRepository<Entidades.Produto>
{
    Task<IEnumerable<Entidades.Produto>> ObterTodos();
    Task<Entidades.Produto> ObterPorId(Guid id);
    Task<IEnumerable<Entidades.Produto>> ObterPorCategoria(int codigo);
    Task<IEnumerable<Categoria>> ObterCategorias();
    void Adicionar(Entidades.Produto produto);
    void Atualizar(Entidades.Produto produto);
    void Adicionar(Categoria categoria);
    void Atualizar(Categoria categoria);
}