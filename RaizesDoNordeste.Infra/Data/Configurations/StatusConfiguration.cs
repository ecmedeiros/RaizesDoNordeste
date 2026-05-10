using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            {
                builder.ToTable("Status");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.Nome)
                    .HasMaxLength(100)
                    .IsRequired();

                builder.HasData(
                    new Status { Id = 1, Nome = "Pedido Realizado" },
                    new Status { Id = 2, Nome = "Confirmado" },
                    new Status { Id = 3, Nome = "Em Preparo" },
                    new Status { Id = 4, Nome = "Pronto" },
                    new Status { Id = 5, Nome = "Em Entrega" },
                    new Status { Id = 6, Nome = "Concluido" },
                    new Status { Id = 7, Nome = "Cancelado" }
                );
            }
        }
    }
}
