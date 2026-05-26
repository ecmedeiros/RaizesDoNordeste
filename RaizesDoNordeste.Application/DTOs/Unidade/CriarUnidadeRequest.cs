namespace RaizesDoNordeste.App.DTOs.Unidade
{
    public class CriarUnidadeRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string? Telefone { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public TimeOnly HorarioAbertura { get; set; }
        public TimeOnly HorarioFechamento { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
