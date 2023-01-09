using AutoMapper;
using DDDPratica.Catalogo.Application.DTO_s;
using DDDPratica.Catalogo.Domain.Produto.Entidades;

namespace DDDPratica.Catalogo.Application.AutoMapper;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Produto, ProdutoDto>()
            .ForMember(d => d.Altura,
                o => o.MapFrom(s => s.Dimensoes.Altura))
            .ForMember(d => d.Largura,
                o => o.MapFrom(s => s.Dimensoes.Largura))
            .ForMember(d => d.Profundidade,
                o => o.MapFrom(s => s.Dimensoes.Profundidade));
        CreateMap<Categoria, CategoriaDto>(); 
    }
}