using Cliente.API.Data.Interfaces;
using Cliente.API.Data.Table;
using Microsoft.EntityFrameworkCore;

namespace Cliente.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteContext _context;

        public ClienteRepository(ClienteContext context)
        {
            _context = context;
        }

        public async Task AdicionarClienteAsync(TbCliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TbCliente>> ListarClienteAsync()
            => await _context.Clientes
            .Include(x => x.Endereco)
            .ToListAsync();

        public async Task<TbCliente> ClientePorIdAsync(int id)
            => await _context.Clientes
            .Include(x => x.Endereco)
            .FirstOrDefaultAsync(c => c.ClienteId == id);

        public async Task AtualizarClienteAsync(TbCliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarClienteAsync(TbCliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }




    }
}
