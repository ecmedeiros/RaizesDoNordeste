namespace RaizesDoNordeste.App.DTOs.Auth
{
    public class LoginResponse
    {
        public string AcessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public UsuarioResponse Usuario { get; set; } = null!;
    }
}
