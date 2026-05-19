using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorEmail(string email);
        Task<Usuario?> ObterPorId(Guid id);
        Task Adicionar(Usuario usuario);
    }
}
