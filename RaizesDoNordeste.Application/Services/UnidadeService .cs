using RaizesDoNordeste.App.DTOs.Unidade;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace RaizesDoNordeste.App.Services
{
    public class UnidadeService(IUnidadeRepository unidadeRepository)
    {
        public async Task<UnidadeResponse> ObterPorId(Guid id)
        {
            var unidade = await unidadeRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Unidade não encontrado.");

            return new UnidadeResponse
            {
                Id = unidade.Id,
                Nome = unidade.Nome,
                Bairro = unidade.Bairro,
                CEP = unidade.CEP,
                Cidade = unidade.Cidade,
                CNPJ = unidade.CNPJ,
                Complemento = unidade.Complemento,
                Email = unidade.Email,
                HorarioAbertura = unidade.HorarioAbertura,
                HorarioFechamento = unidade.HorarioFechamento,
                Logradouro = unidade.Logradouro,
                Numero = unidade.Numero,
                Telefone = unidade.Telefone,
                UF = unidade.UF,
                Ativo = unidade.Ativo
            };
        }

        public async Task<IEnumerable<UnidadeResponse>> ObterTodos(int page, int limit)
        {
            var unidades = await unidadeRepository.ObterTodos(page, limit);

            return unidades.Select(p => new UnidadeResponse
            {
                Id = p!.Id,
                Nome = p!.Nome,
                Bairro = p!.Bairro,
                CEP = p!.CEP,
                Cidade = p!.Cidade,
                CNPJ = p!.CNPJ,
                Complemento = p!.Complemento,
                Email = p!.Email,
                HorarioAbertura = p!.HorarioAbertura,
                HorarioFechamento = p!.HorarioFechamento,
                Logradouro = p!.Logradouro,
                Numero = p!.Numero,
                Telefone = p!.Telefone,
                UF = p!.UF,
                Ativo = p!.Ativo
            });
        }

        public async Task<UnidadeResponse> Adicionar(CriarUnidadeRequest unidade)
        {
            var novoUnidade = new Unidade
            {
                Nome = unidade.Nome,
                Bairro = unidade.Bairro,
                CEP = unidade.CEP,
                Cidade = unidade.Cidade,
                CNPJ = unidade.CNPJ,
                Complemento = unidade.Complemento,
                Email = unidade.Email,
                HorarioAbertura = unidade.HorarioAbertura,
                HorarioFechamento = unidade.HorarioFechamento,
                Logradouro = unidade.Logradouro,
                Numero = unidade.Numero,
                Telefone = unidade.Telefone,
                UF = unidade.UF,
                Ativo = unidade.Ativo
            };

            if (unidade.CNPJ.Length < 14) throw new InvalidOperationException("CNPJ Inválido.");
            
            if (string.IsNullOrEmpty(unidade.Nome)) throw new InvalidOperationException("Nome não pode estar vazio.");

            if (unidade.CEP.Length < 8) throw new InvalidOperationException("CEP Inválido.");

            if (!string.IsNullOrEmpty(unidade.Email))
            {
                string padrao = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                var emailValido = Regex.IsMatch(unidade.Email, padrao, RegexOptions.IgnoreCase);
                if (!emailValido) throw new InvalidOperationException("E-mail inválido");
            }
            if (!string.IsNullOrEmpty(unidade.Telefone))
            {
                if (unidade.Telefone.Length < 10) throw new InvalidOperationException("Telefone inválido");
            }
            if (!string.IsNullOrEmpty(unidade.UF))
            {
                if (unidade.UF.Length < 2) throw new InvalidOperationException("UF inválido");
            }

            await unidadeRepository.Adicionar(novoUnidade);

            return new UnidadeResponse
            {
                Id = novoUnidade.Id,
                Nome = novoUnidade.Nome,
                Bairro = novoUnidade.Bairro,
                CEP = novoUnidade.CEP,
                Cidade = novoUnidade.Cidade,
                CNPJ = novoUnidade.CNPJ,
                Complemento = novoUnidade.Complemento,
                Email = novoUnidade.Email,
                HorarioAbertura = novoUnidade.HorarioAbertura,
                HorarioFechamento = novoUnidade.HorarioFechamento,
                Logradouro = novoUnidade.Logradouro,
                Numero = novoUnidade.Numero,
                Telefone = novoUnidade.Telefone,
                UF = novoUnidade.UF,
                Ativo = novoUnidade.Ativo
            };
        }

        public async Task Deletar(Guid id)
        {
            var unidade = await unidadeRepository.ObterPorId(id) ?? throw new KeyNotFoundException(
                    "Unidade não encontrada."
                );

            unidade.Ativo = false;

            await unidadeRepository.Atualizar(unidade);

            return;
        }

        public async Task<UnidadeResponse> Atualizar(
        Guid id, AtualizarUnidadeRequest request)
        {
            var unidade = await unidadeRepository.ObterPorId(id) ?? throw new KeyNotFoundException(
                    "Unidade não encontrado."
                );

            if (!string.IsNullOrEmpty(request.Nome)) unidade.Nome = request.Nome;
            if (!string.IsNullOrEmpty(request.Email)) unidade.Email = request.Email;
            if (!string.IsNullOrEmpty(request.CNPJ) && request.CNPJ.Length >= 14) unidade.CNPJ = request.CNPJ;

            await unidadeRepository.Atualizar(unidade);

            return new UnidadeResponse
            {
                Id = unidade.Id,
                Nome = unidade.Nome,
                Bairro = unidade.Bairro,
                CEP = unidade.CEP,
                Cidade = unidade.Cidade,
                CNPJ = unidade.CNPJ,
                Complemento = unidade.Complemento,
                Email = unidade.Email,
                HorarioAbertura = unidade.HorarioAbertura,
                HorarioFechamento = unidade.HorarioFechamento,
                Logradouro = unidade.Logradouro,
                Numero = unidade.Numero,
                Telefone = unidade.Telefone,
                UF = unidade.UF,
                Ativo = unidade.Ativo
            };
        }
    }
}
