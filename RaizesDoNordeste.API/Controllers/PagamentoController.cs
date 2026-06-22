using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.App.DTOs.Pagamento;
using RaizesDoNordeste.App.Services;
namespace RaizesDoNordeste.API.Controllers
{
    [ApiController]
    [Route("api/pagamentos")]
    [Authorize]
    public class PagamentosController(PagamentoService pagamentoService)
        : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Cliente,Admin,Gerente")]
        public async Task<IActionResult> Processar(
            [FromBody] PagamentoRequest request)
        {
            var response = await pagamentoService.ProcessarAsync(request);
            return Ok(response);
        }
    }
}