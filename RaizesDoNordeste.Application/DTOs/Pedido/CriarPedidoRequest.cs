using System.ComponentModel.DataAnnotations;

namespace RaizesDoNordeste.App.DTOs.Pedido
{

    public class CriarPedidoRequest
    {
        [Required(ErrorMessage = "O campo canalPedido é obrigatório.")]
        public int? IdCanalPedido { get; set; }

        [Required(ErrorMessage = "O campo idUnidade é obrigatório.")]
        public Guid? IdUnidade { get; set; }

        public bool EhEntrega { get; set; }
        public bool UsarPontos { get; set; }
        public string Observacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O pedido deve ter ao menos um item.")]
        [MinLength(1, ErrorMessage = "O pedido deve ter ao menos um item.")]
        public List<ItensPedidoRequest> ItensPedido { get; set; } = [];
    }
    public class ItensPedidoRequest
    {
        public int Quantidade { get; set; }
        public Guid IdProduto { get; set; }
    }
}
