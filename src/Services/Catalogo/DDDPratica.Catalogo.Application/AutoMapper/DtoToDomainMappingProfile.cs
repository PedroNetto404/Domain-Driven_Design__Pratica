using AutoMapper;
using DDDPratica.Catalogo.Application.DTO_s;
using DDDPratica.Catalogo.Domain.Produto.Entidades;
using DDDPratica.Catalogo.Domain.Produto.ObjetosDeValor;

namespace DDDPratica.Catalogo.Application.AutoMapper;

public class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile()
    {
        CreateMap<ProdutoDto, Produto>()
            .ConstructUsing
            (c => new Produto
                (
                    c.Nome,
                    c.Descricao,
                    c.Ativo,
                    c.Valor,
                    c.DataCadastro,
                    c.CategoriaId,
                    c.Imagem,
                    new Dimensoes(c.Altura, c.Largura, c.Profundidade)
                )
            );
        CreateMap<CategoriaDto, Categoria>()
            .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
    }
}