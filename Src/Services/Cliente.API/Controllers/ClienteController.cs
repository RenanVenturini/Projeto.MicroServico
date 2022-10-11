using Cliente.API.Models.Request;
using Cliente.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cliente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarClienteAsync(ClienteRequest clienteRequest)
        {
            await _clienteService.AdicionarClienteAsync(clienteRequest);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarClienteAsync()
        {
            var cliente = await _clienteService.ListarClienteAsync();
            return Ok(cliente);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ClientePorIdAsync(int id)
        {
            if (id < 1)
                return StatusCode(StatusCodes.Status400BadRequest);

            var cliente = await _clienteService.ClientePorIdAsync(id);

            if (cliente == default)
                return StatusCode(StatusCodes.Status404NotFound);

            return Ok(cliente);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarClienteAsync(AtualizarClienteRequest atualizarClienteRequest)
        {
            await _clienteService.AtualizarClienteAsync(atualizarClienteRequest);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}/deletar")]
        public async Task<IActionResult> DeletarClienteAsync(int id)
        {
            await _clienteService.DeletarClienteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }


    }
}
