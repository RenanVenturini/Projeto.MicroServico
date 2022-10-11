using AutoMapper;
using Cliente.API.Data.Table;
using Cliente.API.Models.Response;
using Cliente.API.Models.Request;

namespace Cliente.API.Data.Mappings.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteRequest, TbCliente>();
            CreateMap<AtualizarClienteRequest, TbCliente>();
            CreateMap<TbCliente, ClienteResponse>()
                .ForMember(
                    dest => dest.Id, 
                    opt => opt.MapFrom(src => src.ClienteId)
                );
        }
    }
}
