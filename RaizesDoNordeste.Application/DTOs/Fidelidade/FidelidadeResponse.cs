namespace RaizesDoNordeste.App.DTOs.Fidelidade
{
    public class FidelidadeResponse
    {
        public Guid IdUsuario { get; set; }
        public int SaldoPontos { get; set; }
        public DateTime? Validade { get; set; }
    }
}
