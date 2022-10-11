using Cliente.API.Models.Response;
using Cliente.API.Models.Request;

namespace Cliente.API.Services.Interfaces
{
    public interface IClienteService
    {
        Task AdicionarClienteAsync(ClienteRequest clienteRequest);
        Task<IEnumerable<ClienteResponse>> ListarClienteAsync();
        Task<ClienteResponse> ClientePorIdAsync(int id);
        Task AtualizarClienteAsync(AtualizarClienteRequest atualizarClienteRequest);
        Task DeletarClienteAsync(int id);
    }
}
