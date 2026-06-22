public class PagamentoResponse
{
    public Guid IdPedido { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Timestamp { get; set; }
}