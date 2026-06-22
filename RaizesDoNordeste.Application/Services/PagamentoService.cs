using RaizesDoNordeste.App.DTOs.Pagamento;
using RaizesDoNordeste.App.Services;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.Interfaces;

public class PagamentoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly FidelidadeService _fidelidadeService;

    public PagamentoService(
        IPedidoRepository pedidoRepository,
        FidelidadeService fidelidadeService)
    {
        _pedidoRepository = pedidoRepository;
        _fidelidadeService = fidelidadeService;
    }

    public async Task<PagamentoResponse> ProcessarAsync(PagamentoRequest request)
    {
        var pedido = await _pedidoRepository.ObterPorId(request.IdPedido);

        if (pedido == null)
            throw new KeyNotFoundException("Pedido não encontrado.");

        if (pedido.IdStatusPagamento != (int)StatusPagamento.AguardandoPagamento)
            throw new InvalidOperationException(
                "Pedido não está aguardando pagamento."
            );

        var aprovado = new Random().Next(1, 101) <= 80;

        if (aprovado)
        {
            pedido.IdStatusPagamento = (int)StatusPagamento.PagamentoAprovado;
            pedido.IdStatus = (int)Status.Confirmado;

            // Adiciona pontos se pagamento aprovado
            await _fidelidadeService.AdicionarPontosAsync(
                pedido.IdUsuario,
                pedido.PrecoTotal
            );
        }
        else
        {
            pedido.IdStatusPagamento = (int)StatusPagamento.PagamentoRecusado;
        }

        await _pedidoRepository.Atualizar(pedido);

        return new PagamentoResponse
        {
            IdPedido = pedido.Id,
            Status = aprovado ? "APROVADO" : "RECUSADO",
            Mensagem = aprovado
                ? "Pagamento processado com sucesso."
                : "Pagamento recusado. Tente novamente.",
            Valor = pedido.PrecoTotal,
            Timestamp = DateTime.UtcNow
        };
    }
}