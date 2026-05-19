namespace RaizesDoNordeste.App.DTOs.Auth
{
    public class RegistroRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool AceitouTermos { get; set; }
        public bool ConsentimentoFidelidade { get; set; }
    }
}
