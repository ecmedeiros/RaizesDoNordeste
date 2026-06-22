using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Domain.Interfaces
{
    public interface IEstoqueRepository
    {
        Task<IEnumerable<Estoque>> ObterPorUnidadeAsync(Guid idUnidade);
        Task<Estoque?> ObterPorUnidadeEProdutoAsync(Guid idUnidade, Guid idProduto);
        Task AdicionarAsync(Estoque estoque);
        Task AtualizarAsync(Estoque estoque);
        Task<IEnumerable<Estoque>> ObterPorIds(List<Guid> itensIds);
    }
}