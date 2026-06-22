using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.App.DTOs.Pedido
{
    public class PedidoResponse
    {
        public Guid Id { get; set; }
        public int CanalPedido { get; set; }
        public int IdStatus { get; set; }
        public int IdStatusPagamento { get; set; }
        public decimal PrecoTotal { get; set; }
        public decimal PrecoDesconto { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdUnidade { get; set; }
        public bool EhEntrega { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public List<ItensPedidoResponse> Itens { get; set; } = [];
    }

    public class ItensPedidoResponse
    {
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
