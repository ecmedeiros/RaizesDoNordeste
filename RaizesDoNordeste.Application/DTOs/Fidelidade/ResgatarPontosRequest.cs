using System.ComponentModel.DataAnnotations;

namespace RaizesDoNordeste.App.DTOs.Fidelidade
{
    public class ResgatarPontosRequest
    {
        [Required(ErrorMessage = "O campo pontosResgatar é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
        public int? PontosResgatar { get; set; }
    }
}
