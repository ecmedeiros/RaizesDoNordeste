namespace RaizesDoNordeste.Domain.Entidades
{
    public class ItensPedido : EntidadeBase
    {
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public Guid IdProduto { get; set; }
        public Guid IdPedido { get; set; }

        public Pedido Pedido { get; set; } = null!;
        public Produto Produto { get; set; } = null!;
    }
}
