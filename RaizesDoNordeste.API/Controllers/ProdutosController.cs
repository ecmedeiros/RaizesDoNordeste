using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.App.DTOs.Produto;
using RaizesDoNordeste.App.Services;

namespace RaizesDoNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProdutosController(ProdutoService produtoService) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos([FromQuery] int page, [FromQuery] int limit)
        {
            var produtos = produtoService.ObterTodos(page, limit);

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var produto = await produtoService.ObterPorId(id);
                return Ok(produto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    error = "PRODUTO_NAO_ENCONTRADO",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = $"/api/produtos/{id}"
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> Adicionar([FromBody] CriarProdutoRequest request)
        {
            try
            {
                var produtoNovo = await produtoService.Adicionar(request);

                return CreatedAtAction(nameof(ObterPorId), new { id = produtoNovo.Id }, produtoNovo);


            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    error = "ERRO_CRIAR_PRODUTO",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = "/api/produtos"
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> Atualizar(
        Guid id,
        [FromBody] AtualizarProdutoRequest request)
        {
            try
            {
                var produto = await produtoService.Atualizar(id, request);
                return Ok(produto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    error = "PRODUTO_NAO_ENCONTRADO",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = $"/api/produtos/{id}"
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    error = "ERRO_ATUALIZAR_PRODUTO",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = $"/api/produtos/{id}"
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin,Gerente")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await produtoService.Deletar(id);
                return NoContent();
            }catch(KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    error = "PRODUTO_NAO_ENCONTRADO",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = "/api/produtos"
                });
            }
        }

    }
}