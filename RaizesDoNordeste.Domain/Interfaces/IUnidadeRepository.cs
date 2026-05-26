using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Domain.Interfaces
{
    public interface IUnidadeRepository
    {
        Task<IEnumerable<Unidade?>> ObterTodos(int page, int limit);
        Task<Unidade?> ObterPorId(Guid id);
        Task Adicionar(Unidade unidade);
        Task Atualizar(Unidade unidade);
    }
}
