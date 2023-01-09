using DDDPratica.Catalogo.Domain.Produto.ObjetosDeValor;
using DDDPratica.Core.DomainObjects;
using DDDPratica.Core.DomainObjects.Entidades;
using DDDPratica.Core.DomainObjects.Exceptions;
using DDDPratica.Core.DomainObjects.Validações;

namespace DDDPratica.Catalogo.Domain.Produto.Entidades;

public class Produto : Entidade, IRaizAgregacao
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public string Imagem { get; private set; }
    public int QuantidadeEstoque { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; }
    public Dimensoes Dimensoes { get; private set; }

    public Produto
    (
        string nome, 
        string descricao,
        bool ativo,
        decimal valor, 
        DateTime dataCadastro, 
        Guid categoriaId,
        string imagem, 
        Dimensoes dimensoes
    )
    {
        Nome = nome;
        Descricao = descricao;
        Ativo = ativo;
        Valor = valor;
        DataCadastro = dataCadastro;
        Imagem = imagem;
        Dimensoes = dimensoes;
        
        Validar();
    }

    protected Produto()
    {
        
    }

    public void Ativar() => Ativo = true;
    
    public void Desativar() => Ativo = false;
    
    public void AlterarCategoria(Categoria categoria) => Categoria = categoria;

    public void AlterarDescricao(string descricao)
    {
        Validacoes.ValidarSeVazio(descricao, "O campo Descricao do produto não pode estar vazio");

        Descricao = descricao;
    }

    public void DebitarEstoque(int quantidade)
    {
        if (quantidade < 0) quantidade *= -1;

        if (!PossuiEstoque(quantidade))
            throw new DomainException("Estoque insuficiente");
        
        QuantidadeEstoque -= quantidade; 
    }

    public void ReporEstoque(int quantidade)
    {
        if (quantidade < 0)
            throw new DomainException("Não é possível repor uma quantidade negativa ao estoque.");
        
        QuantidadeEstoque += quantidade; 
    }

    public bool PossuiEstoque(int quantidade)
    {
        return QuantidadeEstoque >= quantidade; 
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, "O campo Nome do produto não deve estar vazio.");
        Validacoes.ValidarSeVazio(Descricao, "O campo Descricao do produto não pode estar vazio");
        Validacoes.ValidarSeMenorIgualMinimo(Valor, 0, "O campo Valor do produto não pode ser menor ou igual a 0");
        Validacoes.ValidarSeVazio(Imagem, "O campo Imagem do produto não pode estar vazio");
    }
}