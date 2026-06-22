using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ObterTodos(int? idStatus, int? idCanalPedido, int page, int limit);
        Task<Pedido?> ObterPorId(Guid id);
        Task Adicionar(Pedido pedido);
        Task Atualizar(Pedido pedido);
    }
}
