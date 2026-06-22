using RaizesDoNordeste.App.DTOs.Pedido;
using RaizesDoNordeste.Domain.Entidades;
using Status = RaizesDoNordeste.Domain.Enums.Status;
using StatusPagamento = RaizesDoNordeste.Domain.Enums.StatusPagamento;
using CanalPedido = RaizesDoNordeste.Domain.Enums.CanalPedido;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.App.Services
{
    public class PedidoService(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository, IUnidadeRepository unidadeRepository, IEstoqueRepository estoqueRepository)
    {
        public async Task<PedidoResponse> ObterPorId(Guid id)
        {
            var pedido = await pedidoRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Pedido não encontrado.");

            return new PedidoResponse
            {
                Id = pedido.Id,
                CanalPedido = pedido.CanalPedido,
                EhEntrega = pedido.EhEntrega,
                IdStatus = pedido.IdStatus,
                IdStatusPagamento = pedido.IdStatusPagamento,
                IdUnidade = pedido.IdUnidade,
                IdUsuario = pedido.IdUsuario,
                Observacao = pedido.Observacao,
                PrecoDesconto = pedido.PrecoDesconto,
                PrecoTotal = pedido.PrecoTotal,
                Itens = [.. pedido.Itens.Select(i => new ItensPedidoResponse
                {
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                })]
            };
        }

        public async Task<IEnumerable<PedidoResponse>> ObterTodos(int? idStatus, int? idCanalPedido, int page, int limit)
        {
            if (limit == 0) limit = 10; // Valor padrão para limitar resultados
            var pedidos = await pedidoRepository.ObterTodos(idStatus, idCanalPedido, page, limit);

            return pedidos.Select(p => new PedidoResponse
            {
                Id = p!.Id,
                CanalPedido = p.CanalPedido,
                EhEntrega = p.EhEntrega,
                IdStatus = p.IdStatus,
                IdStatusPagamento = p.IdStatusPagamento,
                IdUnidade = p.IdUnidade,
                IdUsuario = p.IdUsuario,
                Observacao = p.Observacao,
                PrecoDesconto = p.PrecoDesconto,
                PrecoTotal = p.PrecoTotal,
                Itens = [.. p.Itens.Select(i => new ItensPedidoResponse
                {
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                })]
            });
        }

        public async Task Atualizar(Guid id, int idStatus)
        {
            var pedido = await pedidoRepository.ObterPorId(id) ?? throw new KeyNotFoundException("Pedido não encontrado.");

            // Valida transição de status
            var transicoesValidas = new Dictionary<int, List<int>>
            {
                { 1, new List<int> { 2, 7 } },  // Pedido Realizado → Confirmado ou Cancelado
                { 2, new List<int> { 3, 7 } },  // Confirmado → Em Preparo ou Cancelado
                { 3, new List<int> { 4, 7 } },  // Em Preparo → Pronto ou Cancelado
                { 4, new List<int> { 5, 6 } },  // Pronto → Em Entrega ou Concluido
                { 5, new List<int> { 6 } },     // Em Entrega → Concluido
            };

            if (!transicoesValidas.TryGetValue(pedido.IdStatus, out List<int>? value) || !value.Contains(idStatus))
                throw new InvalidOperationException(
                    $"Transição de status inválida."
                );

            pedido.IdStatus = idStatus;
            await pedidoRepository.Atualizar(pedido);
        }

        public async Task<PedidoResponse> Adicionar(CriarPedidoRequest pedido, Guid idUsuario)
        {
            if (!Enum.IsDefined(typeof(CanalPedido), pedido.IdCanalPedido!.Value))
                throw new ArgumentException(
                    "canalPedido inválido. Valores aceitos: 1 (APP), 2 (TOTEM), 3 (BALCAO), 4 (WEB)."
                );

            var itensIds = pedido.ItensPedido.Select(i => i.IdProduto).ToList();
            var produtos = await produtoRepository.ObterPorIds(itensIds);

            if (produtos.Count() != itensIds.Count)
                throw new KeyNotFoundException("Um ou mais produtos não foram encontrados.");

            var unidadeValida = await unidadeRepository.ObterPorId(pedido.IdUnidade!.Value);
            if (unidadeValida == null)
                throw new KeyNotFoundException("Unidade não encontrada.");

            if (pedido.ItensPedido.Any(i => i.Quantidade <= 0))
                throw new InvalidOperationException("A quantidade de cada item deve ser maior que zero.");

            var estoques = await estoqueRepository.ObterPorIds(itensIds);

            var produtosSemEstoque = pedido.ItensPedido
                .Where(item =>
                {
                    var estoque = estoques
                        .FirstOrDefault(e => e.IdProduto == item.IdProduto);

                    return estoque == null || estoque.Quantidade < item.Quantidade;
                })
                .Select(item => item.IdProduto)
                .ToList();

            if (produtosSemEstoque.Any())
            {
                throw new InvalidOperationException(
                    $"Produtos sem estoque suficiente: {string.Join(", ", produtosSemEstoque)}");
            }

            var valorTotal = pedido.ItensPedido.Sum(i =>
            {
                var produto = produtos.FirstOrDefault(p => p.Id == i.IdProduto);
                return (produto != null) ? produto.Preco * i.Quantidade : 0;
            });

            decimal valorDesconto = 0;
            if (pedido.UsarPontos)
            {
                //mock
                var pontosUsuario = 87; // Exemplo, deve ser obtido do repositório de usuários    
                valorDesconto = pontosUsuario * 0.1m;
                valorTotal -= valorDesconto;
            }

            var novoPedido = new Pedido
            {
                CanalPedido = pedido.IdCanalPedido.Value,
                EhEntrega = pedido.EhEntrega,
                IdStatus = (int)Status.PedidoRealizado,
                IdStatusPagamento = (int)StatusPagamento.AguardandoPagamento,
                IdUnidade = pedido.IdUnidade!.Value,
                IdUsuario = idUsuario,
                Observacao = pedido.Observacao,
                PrecoDesconto = valorDesconto,
                PrecoTotal = valorTotal,
                Itens = [.. pedido.ItensPedido.Select(i => new ItensPedido
                {
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = produtos.FirstOrDefault(p => p.Id == i.IdProduto)?.Preco ?? 0
                })]
            };

            if (novoPedido.PrecoTotal <= 0) throw new InvalidOperationException("Preço deve ser maior que zero.");

            await pedidoRepository.Adicionar(novoPedido);

            return new PedidoResponse
            {
                Id = novoPedido.Id,
                CanalPedido = novoPedido.CanalPedido,
                EhEntrega = novoPedido.EhEntrega,
                IdStatus = novoPedido.IdStatus,
                IdStatusPagamento = novoPedido.IdStatusPagamento,
                IdUnidade = novoPedido.IdUnidade,
                IdUsuario = novoPedido.IdUsuario,
                Observacao = novoPedido.Observacao,
                PrecoDesconto = novoPedido.PrecoDesconto,
                PrecoTotal = novoPedido.PrecoTotal,
                Itens = [.. pedido.ItensPedido.Select(i => new ItensPedidoResponse
                {
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = produtos.FirstOrDefault(p => p.Id == i.IdProduto)?.Preco ?? 0
                })]
            };
        }
    }
}
