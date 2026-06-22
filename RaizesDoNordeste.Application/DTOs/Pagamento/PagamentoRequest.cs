namespace RaizesDoNordeste.App.DTOs.Pagamento
{
    public class PagamentoRequest
    {
        public Guid IdPedido { get; set; }
        public string FormaPagamento { get; set; } = "MOCK";
    }
}