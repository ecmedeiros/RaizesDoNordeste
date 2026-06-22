using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.App.DTOs.Pedido;
using RaizesDoNordeste.App.Services;
using System.Security.Claims;

namespace RaizesDoNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PedidosController(PedidoService pedidoService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> ObterTodos([FromQuery] int? idStatus, [FromQuery] int? idCanalPedido, [FromQuery] int page, [FromQuery] int limit)
        {
            var pedidos = pedidoService.ObterTodos(idStatus, idCanalPedido, page, limit);

            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Cliente,Admin,Gerente")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var pedido = await pedidoService.ObterPorId(id);
                return Ok(pedido);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    error = "PEDIDO_NAO_ENCONTRADO",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = $"/api/pedidos/{id}"
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Cliente,Admin,Gerente")]
        public async Task<IActionResult> Adicionar([FromBody] CriarPedidoRequest request)
        {
            var idUsuario = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var pedido = await pedidoService.Adicionar(request, idUsuario);

            return CreatedAtAction(nameof(ObterPorId), new { id = pedido.Id }, pedido);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> AtualizarStatus(Guid id, [FromBody] int idStatus)
        {
            await pedidoService.Atualizar(id, idStatus);
            return NoContent();
        }
    }
}