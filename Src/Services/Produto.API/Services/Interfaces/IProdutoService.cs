using Produto.API.Models.Request;
using Produto.API.Models.Response;

namespace Produto.API.Services.Interfaces
{
    public interface IProdutoService
    {
        Task AdicionarProdutoAsync(ProdutoRequest produtoRequest);
        Task<IEnumerable<ProdutoResponse>> ListarProdutoAsync();
        Task<ProdutoResponse> ProdutoPorIdAsync(int id);
        Task AtualizarProdutoAsync(AtualizaProdutoRequest atualizaProdutoRequest);
        Task DeletarProduto(int id);
    }
}
