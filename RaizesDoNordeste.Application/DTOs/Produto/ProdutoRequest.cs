namespace RaizesDoNordeste.App.DTOs.Produto
{
    public class ProdutoRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
