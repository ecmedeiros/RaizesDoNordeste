namespace RaizesDoNordeste.App.DTOs.Produto
{
    public class ProdutoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public decimal Preco { get; set; }
    }
}
