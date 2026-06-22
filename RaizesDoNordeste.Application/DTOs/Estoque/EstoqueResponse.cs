namespace RaizesDoNordeste.App.DTOs.Estoque
{
    public class EstoqueResponse
    {
        public Guid Id { get; set; }
        public Guid IdUnidade { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}