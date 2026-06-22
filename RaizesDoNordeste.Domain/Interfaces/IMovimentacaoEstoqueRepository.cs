using RaizesDoNordeste.Domain.Entidades;


// Domain/Interfaces/IMovimentacaoEstoqueRepository.cs
namespace RaizesDoNordeste.Domain.Interfaces
{
    public interface IMovimentacaoEstoqueRepository
    {
        Task AdicionarAsync(MovimentacaoEstoque movimentacao);
    }
}