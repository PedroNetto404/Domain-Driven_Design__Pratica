using DDDPratica.Core.DomainObjects;
using DDDPratica.Core.DomainObjects.Validações;

namespace DDDPratica.Catalogo.Domain.Produto.ObjetosDeValor;

public class Dimensoes
{
    public decimal Altura { get; init; }
    public decimal Largura { get; init; }
    public decimal Profundidade { get; init; }

    public Dimensoes(decimal altura, decimal largura, decimal profundidade)
    {
        
        Validacoes.ValidarSeMenorIgualMinimo(altura, 1, "O campo Altura não pode ser menor que 1");
        Validacoes.ValidarSeMenorIgualMinimo(largura,1,"O campo Largura não pode ser menor que 1");
        Validacoes.ValidarSeMenorIgualMinimo(profundidade,1,"O campo Profundidade não pode ser menor que 1");

        Altura = altura;
        Largura = largura;
        Profundidade = profundidade;
    }

    public string DescricaoFormatada() => $"LxAxP: {Largura} x {Altura} x {Profundidade}";

    public override string ToString() => DescricaoFormatada(); 
    
}