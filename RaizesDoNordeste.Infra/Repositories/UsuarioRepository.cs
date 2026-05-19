using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infra.Data;

namespace RaizesDoNordeste.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorEmail(string email)
        {
            return await _context.Usuarios
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> ObterPorId(Guid id)
        {
            return await _context.Usuarios
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Adicionar(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
