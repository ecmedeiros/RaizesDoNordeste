using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.App.DTOs.Estoque;
using RaizesDoNordeste.App.Services;
using System.Security.Claims;

namespace RaizesDoNordeste.API.Controllers
{
    [ApiController]
    [Route("api/estoques")]
    [Authorize(Roles = "Admin,Gerente")]
    public class EstoquesController(EstoqueService estoqueService) : ControllerBase
    {
        [HttpGet("{idUnidade}")]
        public async Task<IActionResult> ObterPorUnidade(Guid idUnidade)
        {
            var estoques = await estoqueService.ObterPorUnidadeAsync(idUnidade);
            return Ok(estoques);
        }

        [HttpPost("entrada")]
        public async Task<IActionResult> Entrada(
            [FromBody] MovimentacaoEstoqueRequest request)
        {
            var idUsuario = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );
            await estoqueService.EntradaAsync(request, idUsuario);
            return NoContent();
        }

        [HttpPost("saida")]
        public async Task<IActionResult> Saida(
            [FromBody] MovimentacaoEstoqueRequest request)
        {
            var idUsuario = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );
            await estoqueService.SaidaAsync(request, idUsuario);
            return NoContent();
        }
    }
}