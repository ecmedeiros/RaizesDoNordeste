// Infrastructure/Repositories/PontosUsuarioRepository.cs
using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infra.Data;

namespace RaizesDoNordeste.Infra.Repositories
{
    public class PontosUsuarioRepository : IPontosUsuarioRepository
    {
        private readonly AppDbContext _context;

        public PontosUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PontosUsuario?> ObterPorUsuarioAsync(Guid idUsuario)
        {
            return await _context.PontosUsuario.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);
        }

        public async Task AdicionarAsync(PontosUsuario pontos)
        {
            await _context.PontosUsuario.AddAsync(pontos);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(PontosUsuario pontos)
        {
            _context.PontosUsuario.Update(pontos);
            await _context.SaveChangesAsync();
        }
    }
}