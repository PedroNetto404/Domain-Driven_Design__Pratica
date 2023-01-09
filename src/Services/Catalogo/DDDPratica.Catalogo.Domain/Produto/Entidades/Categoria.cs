using DDDPratica.Core.DomainObjects;
using DDDPratica.Core.DomainObjects.Entidades;
using DDDPratica.Core.DomainObjects.Validações;

namespace DDDPratica.Catalogo.Domain.Produto.Entidades;

public class Categoria : Entidade
{

    public string Nome { get; private set; }
    public int Codigo { get; private set; }
    public ICollection<Produto> Produtos;
    public Categoria(string nome, int codigo)
    {
        Nome = nome;
        Codigo = codigo;
        
        Validar();
    }
    
    protected Categoria(){}
    
    public override string ToString()
    {
        return $"{Nome} - {Codigo}";
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, "O campo nome da categoria não pode estar vazio");
        Validacoes.ValidarSeIgual(Codigo, 0, "O campo Codigo da categoria não pode ser 0.");
    }
}