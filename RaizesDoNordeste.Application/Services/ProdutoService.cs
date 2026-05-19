using RaizesDoNordeste.App.DTOs.Auth;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.App.Services
{
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
