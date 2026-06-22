namespace RaizesDoNordeste.App.DTOs.Pedido
{
    public class PedidoRequest
    {
        public int CanalPedido { get; set; }
        public bool EhEntrega { get; set; }
        public bool UsarPontos { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public List<ItensPedidoRequest> ItensPedido { get; set; } = [];
    }
}
