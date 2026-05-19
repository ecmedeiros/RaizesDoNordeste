using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.App.DTOs.Auth;
using RaizesDoNordeste.App.Services;

namespace RaizesDoNordeste.API.Controllers
{
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

        [HttpPost("Registro")]
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