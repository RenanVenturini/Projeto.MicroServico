using Microsoft.AspNetCore.Mvc;
using Produto.API.Models.Request;
using Produto.API.Services.Interfaces;

namespace Produto.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarProdutoAsync(ProdutoRequest produtoRequest)
        {
            await _produtoService.AdicionarProdutoAsync(produtoRequest);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("Lista")]
        public async Task<IActionResult> ListarProdutoAsync()
        {
            var produtos = await _produtoService.ListarProdutoAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProdutoPorIdAsync(int id)
        {
            if (id < 1)
                return StatusCode(StatusCodes.Status400BadRequest);


            var produtos = await _produtoService.ProdutoPorIdAsync(id);

            if (produtos == default)
                return StatusCode(StatusCodes.Status404NotFound);

            return Ok(produtos);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizaProdutoAsync(AtualizaProdutoRequest atualizaProdutoRequest)
        {
            await _produtoService.AtualizarProdutoAsync(atualizaProdutoRequest);
            return StatusCode(StatusCodes.Status200OK);
        }


        [HttpDelete("{id}/deletar")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            await _produtoService.DeletarProduto(id);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
