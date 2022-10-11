using AutoMapper;
using Produto.API.Data.Interfaces;
using Produto.API.Data.Table;
using Produto.API.Models.Request;
using Produto.API.Models.Response;
using Produto.API.Services.Interfaces;

namespace Produto.API.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AdicionarProdutoAsync(ProdutoRequest produtoRequest)
        {
            var produto = _mapper.Map<TbProduto>(produtoRequest);
            await _repository.AdicionarProdutoAsync(produto);
        }

        public async Task<IEnumerable<ProdutoResponse>> ListarProdutoAsync()
        {
            var produtos = await _repository.ListarProdutoAsync();
            return _mapper.Map<IEnumerable<ProdutoResponse>>(produtos);

        }
        public async Task<ProdutoResponse> ProdutoPorIdAsync(int id)
        {
            var produto = await _repository.ProdutoPorIdAsync(id);
            return _mapper.Map<ProdutoResponse>(produto);
        }
        public async Task AtualizarProdutoAsync(AtualizaProdutoRequest atualizaProdutoRequest)
        {
            var produto = _mapper.Map<TbProduto>(atualizaProdutoRequest);
            await _repository.AtualizarProdutoAsync(produto);
        }
        public async Task DeletarProduto(int id)
            => await _repository.DeletarProduto(id);
    }
}
