namespace RaizesDoNordeste.Domain.Entidades
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public ICollection<Estoque> Estoques { get; set; } = new List<Estoque>();

    }
}
