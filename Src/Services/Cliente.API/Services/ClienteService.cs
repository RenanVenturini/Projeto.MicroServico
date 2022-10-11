using AutoMapper;
using Cliente.API.Data.Interfaces;
using Cliente.API.Data.Table;
using Cliente.API.Models.Response;
using Cliente.API.Models.Request;
using Cliente.API.Services.Interfaces;

namespace Cliente.API.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AdicionarClienteAsync(ClienteRequest clienteRequest)
        {
            var cliente = _mapper.Map<TbCliente>(clienteRequest);
            await _repository.AdicionarClienteAsync(cliente);
        }

        public async Task<IEnumerable<ClienteResponse>> ListarClienteAsync()
        {
            var cliente = await _repository.ListarClienteAsync();
            return _mapper.Map <IEnumerable<ClienteResponse>>(cliente);
        }

        public async Task<ClienteResponse> ClientePorIdAsync(int id)
        {
            var cliente = await _repository.ClientePorIdAsync(id);
            return _mapper.Map<ClienteResponse>(cliente);
        }

        public async Task AtualizarClienteAsync(AtualizarClienteRequest atualizarClienteRequest)
        {
            var cliente = _mapper.Map<TbCliente>(atualizarClienteRequest);
            await _repository.AtualizarClienteAsync(cliente);
        }

        public async Task DeletarClienteAsync(int id)
        {
            var cliente = await _repository.ClientePorIdAsync(id);
            await _repository.DeletarClienteAsync(cliente);
        }
    }
}
