namespace RaizesDoNordeste.Domain.Entidades
{
    public class PontosUsuario : EntidadeBase
    {
        public Guid IdUsuario { get; set; }
        public int Quantidade { get; set; }
        public DateTime Validade { get; set; }

        public Usuario Usuario { get; set; } = null!;
    }
}
