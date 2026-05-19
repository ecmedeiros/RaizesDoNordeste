using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.App.DTOs.Auth;
using RaizesDoNordeste.App.Services;

namespace RaizesDoNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProdutoController(ProdutoService produtoService) : ControllerBase
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

    }

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request)
        {
            try
            {
                var response = await authService.Login(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    error = "CREDENCIAIS_INVALIDAS",
                    message = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = "api/auth/login"
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registro([FromBody] RegistroRequest request)
        {
            try
            {
                var response = await authService.Registrar(request);
                return CreatedAtAction(nameof(Login), response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    error = "ERRO_REGISTRO",
                    messate = ex.Message,
                    timestamp = DateTime.UtcNow,
                    path = "api/auth/registro"
                });
            }
        }
    }
}