namespace RaizesDoNordeste.Domain.Entidades
{
    public class EntidadeBase : EntidadeId
    {
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; set; }
    }
}
