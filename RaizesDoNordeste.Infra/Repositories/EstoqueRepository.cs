using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infra.Data;

namespace RaizesDoNordeste.Infra.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly AppDbContext _context;

        public EstoqueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estoque>> ObterPorUnidadeAsync(Guid idUnidade)
        {
            return await _context.Estoques
                .AsNoTracking()
                .Where(e => e.IdUnidade == idUnidade)
                .ToListAsync();
        }

        public async Task<Estoque?> ObterPorUnidadeEProdutoAsync(
            Guid idUnidade, Guid idProduto)
        {
            return await _context.Estoques
                .FirstOrDefaultAsync(e =>
                    e.IdUnidade == idUnidade &&
                    e.IdProduto == idProduto);
        }

        public async Task AdicionarAsync(Estoque estoque)
        {
            await _context.Estoques.AddAsync(estoque);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Estoque estoque)
        {
            _context.Estoques.Update(estoque);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Estoque>> ObterPorIds(List<Guid> itensIds)
        {
            return await _context.Estoques
                .AsNoTracking()
                .Where(e => itensIds.Contains(e.Id))
                .ToListAsync();
        }
    }
}
