using RaizesDoNordeste.App.DTOs.Fidelidade;
using RaizesDoNordeste.App.DTOs.Fidelidade.RaizesDoNordeste.App.DTOs.Fidelidade;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.App.Services
{
    public class FidelidadeService
    {
        private readonly IPontosUsuarioRepository _pontosRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public FidelidadeService(
            IPontosUsuarioRepository pontosRepository,
            IUsuarioRepository usuarioRepository)
        {
            _pontosRepository = pontosRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<FidelidadeResponse> ObterSaldoAsync(Guid idUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorId(idUsuario);

            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            if (!usuario.DataAceiteTermos.HasValue)
                throw new InvalidOperationException(
                    "Usuário não participante do programa de fidelidade."
                );

            var pontos = await _pontosRepository.ObterPorUsuarioAsync(idUsuario);

            return new FidelidadeResponse
            {
                IdUsuario = idUsuario,
                SaldoPontos = pontos?.Quantidade ?? 0,
                Validade = pontos?.Validade
            };
        }

        public async Task<ResgatarPontosResponse> ResgatarAsync(
            Guid idUsuario, ResgatarPontosRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorId(idUsuario);

            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            if (!usuario.DataAceiteTermos.HasValue)
                throw new InvalidOperationException(
                    "Usuário não participante do programa de fidelidade."
                );

            var pontos = await _pontosRepository.ObterPorUsuarioAsync(idUsuario);

            if (pontos == null || pontos.Quantidade < request.PontosResgatar!.Value)
                throw new InvalidOperationException(
                    $"Saldo insuficiente. Disponível: {pontos?.Quantidade ?? 0} pontos."
                );

            if (pontos.Validade < DateTime.UtcNow)
                throw new InvalidOperationException(
                    "Pontos expirados."
                );

            pontos.Quantidade -= request.PontosResgatar.Value;
            await _pontosRepository.AtualizarAsync(pontos);

            // Cada 100 pontos = R$ 5,00 de desconto
            var desconto = (request.PontosResgatar.Value / 100) * 5m;

            return new ResgatarPontosResponse
            {
                PontosResgatados = request.PontosResgatar.Value,
                SaldoRestante = pontos.Quantidade,
                DescontoGerado = desconto
            };
        }

        public async Task AdicionarPontosAsync(Guid idUsuario, decimal valorPedido)
        {
            var usuario = await _usuarioRepository.ObterPorId(idUsuario);

            if (usuario == null || usuario.DataAceiteTermos == null) return;

            // A cada R$ 10,00 gastos = 1 ponto
            var pontosGanhos = (int)(valorPedido / 10);

            if (pontosGanhos <= 0) return;

            var pontos = await _pontosRepository.ObterPorUsuarioAsync(idUsuario);

            if (pontos == null)
            {
                await _pontosRepository.AdicionarAsync(new PontosUsuario
                {
                    IdUsuario = idUsuario,
                    Quantidade = pontosGanhos,
                    Validade = DateTime.UtcNow.AddYears(1)
                });
            }
            else
            {
                pontos.Quantidade += pontosGanhos;
                pontos.Validade = DateTime.UtcNow.AddYears(1);
                await _pontosRepository.AtualizarAsync(pontos);
            }
        }
    }
}