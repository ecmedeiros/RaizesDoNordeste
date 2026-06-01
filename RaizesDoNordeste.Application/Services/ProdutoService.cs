using RaizesDoNordeste.App.DTOs.Pedido;
using RaizesDoNordeste.App.DTOs.Produto;
using RaizesDoNordeste.Domain.Entidades;
using Status = RaizesDoNordeste.Domain.Enums.Status;
using StatusPagamento = RaizesDoNordeste.Domain.Enums.StatusPagamento;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.App.Services
{
    public class PedidoService(IPedidoRepository pedidoRepository)
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
                Itens = pedido.Itens
            };
        }

        public async Task<PedidoResponse> Adicionar(CriarPedidoRequest pedido, Guid idUsuario)
        {
            if(pedido.UsarPontos)
            {
                //mock
                var pontosUsuario = 100; // Exemplo, deve ser obtido do repositório de usuários
                var itensDict =
                var valorTotal = pedido.ItensPedido.Sum(i => i.PrecoUnitario * i.Quantidade);
            }
            var novoPedido = new Pedido
            {
                CanalPedido = pedido.CanalPedido,
                EhEntrega = pedido.EhEntrega,
                IdStatus = (int)Status.PedidoRealizado,
                IdStatusPagamento = (int)StatusPagamento.AguardandoPagamento,
                IdUnidade = pedido.IdUnidade,
                IdUsuario = idUsuario,
                Observacao = pedido.Observacao,
                PrecoDesconto = 0, // Exemplo, deve ser calculado corretamente
                PrecoTotal = pedido.ItensPedido.Sum(i => i.PrecoUnitario * i.Quantidade) // Exemplo, deve ser calculado corretamente
            };

            if (pedido.Preco <= 0) throw new InvalidOperationException("Preço deve ser maior que zero.");


            await pedidoRepository.Adicionar(novoPedido);

            return new PedidoResponse
            {
                Id = novoPedido.Id,
                Nome = novoPedido.Nome,
                Descricao = novoPedido.Descricao,
                Preco = novoPedido.Preco
            };
        }
    }
    public class ProdutoService(IProdutoRepository produtoRepository)
    {
        public async Task<ProdutoResponse> ObterPorId(Guid id)
        {
            var produto = await produtoRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Produto não encontrado.");

            return new ProdutoResponse
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Descricao = produto.Descricao
            };
        }

        public async Task<IEnumerable<ProdutoResponse>> ObterTodos(int page, int limit)
        {
            var produtos = await produtoRepository.ObterTodos(page, limit);

            return produtos.Select(p => new ProdutoResponse
            {
                Id = p!.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco
            });
        }

        public async Task<ProdutoResponse> Adicionar(CriarProdutoRequest produto)
        {
            var novoProduto = new Produto
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco
            };

            if (produto.Preco <= 0) throw new InvalidOperationException("Preço deve ser maior que zero.");

            await produtoRepository.Adicionar(novoProduto);

            return new ProdutoResponse
            {
                Id = novoProduto.Id,
                Nome = novoProduto.Nome,
                Descricao = novoProduto.Descricao,
                Preco = novoProduto.Preco
            };
        }

        public async Task Deletar(Guid id)
        {
            var produto = await produtoRepository.ObterPorId(id) ?? throw new KeyNotFoundException(
                    "Produto não encontrado."
                );

            produto.Ativo = false;

            await produtoRepository.Atualizar(produto);

            return;
        }

        public async Task<ProdutoResponse> Atualizar(
        Guid id, AtualizarProdutoRequest request)
        {
            var produto = await produtoRepository.ObterPorId(id) ?? throw new KeyNotFoundException(
                    "Produto não encontrado."
                );

            if (request.Nome != null) produto.Nome = request.Nome;
            if (request.Descricao != null) produto.Descricao = request.Descricao;
            if (request.Preco <= 0)
                throw new InvalidOperationException(
                    "Preço deve ser maior que zero."
                );
            produto.Preco = request.Preco;

            await produtoRepository.Atualizar(produto);

            return new ProdutoResponse
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Ativo = produto.Ativo,
                Preco = produto.Preco
            };
        }
    }
}
