using Microsoft.EntityFrameworkCore;

namespace RaizesDoNordeste.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro: {Message}", ex.Message);
                await TratarExcecaoAsync(context, ex);
            }
        }

        private static async Task TratarExcecaoAsync(
            HttpContext context, Exception ex)
        {
            var (statusCode, erro) = ex switch
            {
                KeyNotFoundException => (
                    StatusCodes.Status404NotFound,
                    "NAO_ENCONTRADO"
                ),
                UnauthorizedAccessException => (
                    StatusCodes.Status401Unauthorized,
                    "NAO_AUTORIZADO"
                ),
                InvalidOperationException => (
                    StatusCodes.Status409Conflict,
                    "ERRO_NEGOCIO"
                ),
                DbUpdateException e when
                    e.InnerException?.Message.Contains("UNIQUE") == true => (
                    StatusCodes.Status409Conflict,
                    "REGISTRO_DUPLICADO"
                ),
                ArgumentException => (
                    StatusCodes.Status422UnprocessableEntity,
                    "ERRO_VALIDACAO"
                ),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    "ERRO_INTERNO"
                )
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = erro,
                message = ex.Message,
                timestamp = DateTime.UtcNow,
                path = context.Request.Path
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}