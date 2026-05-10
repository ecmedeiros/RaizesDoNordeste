using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class CanalPedidoConfiguration : IEntityTypeConfiguration<CanalPedido>
    {
        public void Configure(EntityTypeBuilder<CanalPedido> builder)
        {
            {
                builder.ToTable("CanalPedidos");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.Nome)
                    .HasMaxLength(100)
                    .IsRequired();

                builder.HasData(
                    new CanalPedido { Id = 1, Nome = "App" },
                    new CanalPedido { Id = 2, Nome = "Totem" },
                    new CanalPedido { Id = 3, Nome = "Balcao" },
                    new CanalPedido { Id = 4, Nome = "Web" }
                );
            }
        }
    }
}
