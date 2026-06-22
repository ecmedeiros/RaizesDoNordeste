using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infra.Data;

namespace RaizesDoNordeste.Infra.Repositories
{
    public class PedidoRepository(AppDbContext context) : IPedidoRepository
    {
        public async Task<IEnumerable<Pedido>> ObterTodos(int? idStatus, int? idCanalPedido, int page, int limit)
        {
            return await context.Pedidos
                .Where(p => (idStatus.HasValue && p.IdStatus == idStatus.Value) && (idCanalPedido.HasValue && p.CanalPedido == idCanalPedido.Value))
                .Include(p => p.Itens)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Pedido?> ObterPorId(Guid id)
        {
            return await context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ObterPorIds(IEnumerable<Guid> ids)
        {
            return await context.Pedidos
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }

        public async Task Adicionar(Pedido pedido)
        {
            await context.Pedidos.AddAsync(pedido);
            await context.SaveChangesAsync();
        }
        public async Task Atualizar(Pedido pedido)
        {
            context.Pedidos.Update(pedido);
            await context.SaveChangesAsync();
        }
    }
}
