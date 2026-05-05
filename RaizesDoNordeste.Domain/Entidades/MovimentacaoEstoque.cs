namespace RaizesDoNordeste.Domain.Entidades
{
    public class MovimentacaoEstoque : EntidadeBase
    {
        public Guid IdEstoque { get; set; }
        public int IdTipoEstoque { get; set; }
        public int Quantidade { get; set; }
        public Guid IdUsuario { get; set; }
        public string Motivo { get; set; } = string.Empty;

        public Estoque Estoque { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
    }
}
