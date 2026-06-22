using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.App.DTOs.Fidelidade;
using RaizesDoNordeste.App.Services;
using System.Security.Claims;

namespace RaizesDoNordeste.API.Controllers
{
    [ApiController]
    [Route("api/fidelidade")]
    [Authorize]
    public class FidelidadeController(FidelidadeService fidelidadeService)
        : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        [Authorize(Roles = "Admin,Gerente,Cliente")]
        public async Task<IActionResult> ObterSaldo(Guid idUsuario)
        {
            var saldo = await fidelidadeService.ObterSaldoAsync(idUsuario);
            return Ok(saldo);
        }

        [HttpPost("resgatar")]
        [Authorize(Roles = "Admin,Gerente,Cliente")]
        public async Task<IActionResult> Resgatar(
            [FromBody] ResgatarPontosRequest request)
        {
            var idUsuario = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );
            var response = await fidelidadeService.ResgatarAsync(idUsuario, request);
            return Ok(response);
        }
    }
}