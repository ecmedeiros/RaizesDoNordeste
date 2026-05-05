namespace RaizesDoNordeste.Domain.Entidades
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
