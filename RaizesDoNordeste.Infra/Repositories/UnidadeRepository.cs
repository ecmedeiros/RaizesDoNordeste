using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infra.Data;

namespace RaizesDoNordeste.Infra.Repositories
{
    public class UnidadeRepository(AppDbContext context) : IUnidadeRepository
    {
        public async Task<IEnumerable<Unidade?>> ObterTodos(int page, int limit)
        {
            return await context.Unidades
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }
        public async Task<Unidade?> ObterPorId(Guid id)
        {
            return await context.Unidades
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task Adicionar(Unidade unidade)
        {
            await context.Unidades.AddAsync(unidade);
            await context.SaveChangesAsync();
        }
        public async Task Atualizar(Unidade unidade)
        {
            context.Unidades.Update(unidade);
            await context.SaveChangesAsync();
        }

    }
}
