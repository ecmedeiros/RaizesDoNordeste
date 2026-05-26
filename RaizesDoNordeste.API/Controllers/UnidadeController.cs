using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.App.DTOs.Unidade;
using RaizesDoNordeste.App.Services;

namespace RaizesDoNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UnidadeController(UnidadeService unidadeService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> ObterTodos([FromQuery] int page, [FromQuery] int limit)
        {
            var unidades = unidadeService.ObterTodos(page, limit);

            return Ok(unidades);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var unidade = await unidadeService.ObterPorId(id);
                return Ok(unidade);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    error = "UNIDADE_NAO_ENCONTRADA",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = $"/api/unidades/{id}"
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> Adicionar([FromBody] CriarUnidadeRequest request)
        {
            try
            {
                var unidadeNovo = await unidadeService.Adicionar(request);

                return CreatedAtAction(nameof(ObterPorId), new { id = unidadeNovo.Id }, unidadeNovo);


            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    error = "ERRO_CRIAR_UNIDADE",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = "/api/unidades"
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> Atualizar(
        Guid id,
        [FromBody] AtualizarUnidadeRequest request)
        {
            try
            {
                var unidade = await unidadeService.Atualizar(id, request);
                return Ok(unidade);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    error = "UNIDADE_NAO_ENCONTRADA",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = $"/api/unidades/{id}"
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    error = "ERRO_ATUALIZAR_UNIDADE",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = $"/api/unidades/{id}"
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin,Gerente")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await unidadeService.Deletar(id);
                return NoContent();
            }catch(KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    error = "UNIDADE_NAO_ENCONTRADA",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = "/api/unidades"
                });
            }
        }

    }
}