using Produto.API.Data.Table;

namespace Produto.API.Data.Interfaces
{
    public interface IProdutoRepository
    {
        Task AdicionarProdutoAsync(TbProduto produto);
        Task<IEnumerable<TbProduto>> ListarProdutoAsync();
        Task<TbProduto> ProdutoPorIdAsync(int id);
        Task AtualizarProdutoAsync(TbProduto produto);
        Task DeletarProduto(int id);
    }
}
