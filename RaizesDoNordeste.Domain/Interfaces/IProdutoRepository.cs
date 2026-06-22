using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto?>> ObterTodos(int page, int limit);
        Task<Produto?> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorIds(IEnumerable<Guid> ids);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
    }
}