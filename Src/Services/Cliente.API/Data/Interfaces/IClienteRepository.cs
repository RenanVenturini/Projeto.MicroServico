using Cliente.API.Data.Table;

namespace Cliente.API.Data.Interfaces
{
    public interface IClienteRepository
    {
        Task AdicionarClienteAsync(TbCliente cliente);
        Task<IEnumerable<TbCliente>> ListarClienteAsync();
        Task<TbCliente> ClientePorIdAsync(int id);
        Task AtualizarClienteAsync(TbCliente cliente);
        Task DeletarClienteAsync(TbCliente cliente);
    }
}
