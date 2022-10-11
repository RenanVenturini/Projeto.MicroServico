using Microsoft.EntityFrameworkCore;
using Produto.API.Data.Interfaces;
using Produto.API.Data.Table;

namespace Produto.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoContext _context;

        public ProdutoRepository(ProdutoContext context)
        {
            _context = context;
        }
        public async Task AdicionarProdutoAsync(TbProduto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TbProduto>> ListarProdutoAsync()
            => await _context.Produtos.ToListAsync();

        public async Task<TbProduto> ProdutoPorIdAsync(int id)
            => await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AtualizarProdutoAsync(TbProduto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarProduto(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            if (produto != null) _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }


    }
}
