using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infra.Data;

namespace RaizesDoNordeste.Infra.Repositories
{
    public class ProdutoRepository(AppDbContext context) : IProdutoRepository
    {
        public async Task<IEnumerable<Produto?>> ObterTodos(int page, int limit)
        {
            return await context.Produtos
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Produto?> ObterPorId(Guid id)
        {
            return await context.Produtos
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterPorIds(IEnumerable<Guid> ids)
        {
            return await context.Produtos
                .AsNoTracking()
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }

        public async Task Adicionar(Produto produto)
        {
            await context.Produtos.AddAsync(produto);
            await context.SaveChangesAsync();
        }
        public async Task Atualizar(Produto produto)
        {
            context.Produtos.Update(produto);
            await context.SaveChangesAsync();
        }
    }
}
