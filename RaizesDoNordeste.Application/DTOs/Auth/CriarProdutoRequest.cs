namespace RaizesDoNordeste.App.DTOs.Auth
{
    public class CriarProdutoRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
