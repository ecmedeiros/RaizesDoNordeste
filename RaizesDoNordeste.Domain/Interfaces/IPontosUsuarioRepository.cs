using RaizesDoNordeste.Domain.Entidades;


// Domain/Interfaces/IMovimentacaoEstoqueRepository.cs
namespace RaizesDoNordeste.Domain.Interfaces
{
    public interface IPontosUsuarioRepository
    {
        Task<PontosUsuario?> ObterPorUsuarioAsync(Guid idUsuario);
        Task AdicionarAsync(PontosUsuario pontos);
        Task AtualizarAsync(PontosUsuario pontos);
    }
}