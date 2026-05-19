namespace RaizesDoNordeste.Domain.Entidades
{
    public class Estoque : EntidadeBase
    {
        public int Quantidade { get; set; }
        public Guid IdProduto { get; set; }
        public Guid IdUnidade { get; set; }

        public Unidade? Unidade { get; set; }
        public Produto? Produto { get; set; }
    }
}
