using AutoMapper;
using Cliente.API.Data.Table;
using Cliente.API.Models.Request;
using Cliente.API.Models.Response;

namespace Cliente.API.Data.Mappings.Profiles
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<EnderecoRequest, TbEnderecoCliente>();
            CreateMap<AtualizarEnderecoRequest, TbEnderecoCliente>();
            CreateMap<TbEnderecoCliente, EnderecoResponse>();
        }
    }
}
