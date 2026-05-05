namespace RaizesDoNordeste.Domain.Entidades
{
    public class Pedido : EntidadeBase
    {
        public int CanalPedido { get; set; }
        public int IdStatus { get; set; }
        public int IdStatusPagamento { get; set; }
        public decimal PrecoTotal { get; set; }
        public decimal PrecoDesconto { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdUnidade { get; set; }
        public bool EhEntrega { get; set; }
        public string Observacao { get; set; } = string.Empty;

        public Usuario Usuario { get; set; } = null!;
        public Unidade Unidade { get; set; } = null!;
        public ICollection<ItensPedido> Itens { get; set; } = new List<ItensPedido>
    }
}
