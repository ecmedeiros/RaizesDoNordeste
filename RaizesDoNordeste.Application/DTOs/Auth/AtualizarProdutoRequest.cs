namespace RaizesDoNordeste.App.DTOs.Auth
{
    public class AtualizarProdutoRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
    }
}
