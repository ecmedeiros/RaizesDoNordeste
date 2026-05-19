using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto?>> ObterTodos(int page, int limit);
        Task<Produto?> ObterPorId(Guid id);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
    }
}
