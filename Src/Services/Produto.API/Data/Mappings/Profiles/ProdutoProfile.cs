using AutoMapper;
using Produto.API.Data.Table;
using Produto.API.Models.Request;
using Produto.API.Models.Response;

namespace Produto.API.Data.Mappings.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<TbProduto, ProdutoResponse>()
                .ForMember(
                    dest => dest.Identificacao,
                    opt => opt.MapFrom(src => src.Nome)
                );
            CreateMap<ProdutoRequest, TbProduto>();
            CreateMap<AtualizaProdutoRequest, TbProduto>();

        }
    }
}
