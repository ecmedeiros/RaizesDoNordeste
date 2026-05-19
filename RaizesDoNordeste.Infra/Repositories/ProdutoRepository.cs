using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infra.Data;

namespace RaizesDoNordeste.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto?>> ObterTodos(int page, int limit)
        {
            return await _context.Produtos
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Produto?> ObterPorId(Guid id)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Adicionar(Produto Produto)
        {
            await _context.Produtos.AddAsync(Produto);
            await _context.SaveChangesAsync();
        }
        public async Task Atualizar(Produto Produto)
        {
            _context.Produtos.Update(Produto);
            await _context.SaveChangesAsync();
        }
    }
}
