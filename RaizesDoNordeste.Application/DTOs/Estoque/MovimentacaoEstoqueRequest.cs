using System.ComponentModel.DataAnnotations;

namespace RaizesDoNordeste.App.DTOs.Estoque
{
    public class MovimentacaoEstoqueRequest
    {
        [Required(ErrorMessage = "O campo idUnidade é obrigatório.")]
        public Guid? IdUnidade { get; set; }

        [Required(ErrorMessage = "O campo idProduto é obrigatório.")]
        public Guid? IdProduto { get; set; }

        [Required(ErrorMessage = "O campo quantidade é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
        public int? Quantidade { get; set; }

        public string Motivo { get; set; } = string.Empty;
    }
}