namespace RaizesDoNordeste.Domain.Entidades
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int IdPerfil { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime? DataAceiteTermos { get; set; }
    }
}
