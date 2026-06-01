namespace RaizesDoNordeste.App.DTOs.Produto
{
    public class CriarPedidoRequest
    {
        public int CanalPedido { get; set; }
        public bool EhEntrega { get; set; }
        public Guid IdUnidade { get; set; }
        public bool UsarPontos { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public List<ItensPedidoRequest> ItensPedido { get; set; } = [];
    }
}
