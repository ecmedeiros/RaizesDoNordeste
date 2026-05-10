using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class StatusPagamentoConfiguration : IEntityTypeConfiguration<StatusPagamento>
    {
        public void Configure(EntityTypeBuilder<StatusPagamento> builder)
        {
            {
                builder.ToTable("StatusPagamentos");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.Nome)
                    .HasMaxLength(100)
                    .IsRequired();

                builder.HasData(
                    new StatusPagamento { Id = 1, Nome = "Aguardando Pagamento" },
                    new StatusPagamento { Id = 2, Nome = "Pagamento Aprovado" },
                    new StatusPagamento { Id = 3, Nome = "Pagamento Recusado" },
                    new StatusPagamento { Id = 4, Nome = "Reembolso Solicitado" },
                    new StatusPagamento { Id = 5, Nome = "Reembolsado" }
                );
            }
        }
    }
}
