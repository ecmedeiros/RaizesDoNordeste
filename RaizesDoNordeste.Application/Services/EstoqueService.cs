using RaizesDoNordeste.App.DTOs.Estoque;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.App.Services
{
    public class EstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IMovimentacaoEstoqueRepository _movimentacaoRepository;

        public EstoqueService(
            IEstoqueRepository estoqueRepository,
            IMovimentacaoEstoqueRepository movimentacaoRepository)
        {
            _estoqueRepository = estoqueRepository;
            _movimentacaoRepository = movimentacaoRepository;
        }

        public async Task<IEnumerable<EstoqueResponse>> ObterPorUnidadeAsync(
            Guid idUnidade)
        {
            var estoques = await _estoqueRepository
                .ObterPorUnidadeAsync(idUnidade);

            return estoques.Select(e => new EstoqueResponse
            {
                Id = e.Id,
                IdUnidade = e.IdUnidade,
                IdProduto = e.IdProduto,
                Quantidade = e.Quantidade
            });
        }

        public async Task EntradaAsync(
            MovimentacaoEstoqueRequest request, Guid idUsuario)
        {
            var estoque = await _estoqueRepository
                .ObterPorUnidadeEProdutoAsync(
                    request.IdUnidade!.Value,
                    request.IdProduto!.Value
                );

            if (estoque == null)
                throw new KeyNotFoundException(
                    "Estoque não encontrado para essa unidade e produto."
                );

            estoque.Quantidade += request.Quantidade!.Value;
            await _estoqueRepository.AtualizarAsync(estoque);

            await _movimentacaoRepository.AdicionarAsync(new MovimentacaoEstoque
            {
                IdEstoque = estoque.Id,
                IdTipoEstoque = 1, // Entrada
                Quantidade = request.Quantidade.Value,
                Motivo = request.Motivo,
                IdUsuario = idUsuario
            });
        }

        public async Task SaidaAsync(
            MovimentacaoEstoqueRequest request, Guid idUsuario)
        {
            var estoque = await _estoqueRepository
                .ObterPorUnidadeEProdutoAsync(
                    request.IdUnidade!.Value,
                    request.IdProduto!.Value
                );

            if (estoque == null)
                throw new KeyNotFoundException(
                    "Estoque não encontrado para essa unidade e produto."
                );

            if (estoque.Quantidade < request.Quantidade!.Value)
                throw new InvalidOperationException(
                    $"Estoque insuficiente. Disponível: {estoque.Quantidade}"
                );

            estoque.Quantidade -= request.Quantidade.Value;
            await _estoqueRepository.AtualizarAsync(estoque);

            await _movimentacaoRepository.AdicionarAsync(new MovimentacaoEstoque
            {
                IdEstoque = estoque.Id,
                IdTipoEstoque = 2, // Saida
                Quantidade = request.Quantidade.Value,
                Motivo = request.Motivo,
                IdUsuario = idUsuario
            });
        }
    }
}