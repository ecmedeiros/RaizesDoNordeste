using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            {
                builder.ToTable("Pedidos");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.IdStatus)
                    .IsRequired();
                builder.Property(u => u.IdStatusPagamento)
                    .IsRequired();
                builder.Property(u => u.PrecoTotal)
                    .IsRequired();
                builder.Property(u => u.PrecoDesconto)
                    .IsRequired();
                builder.Property(u => u.IdUsuario)
                    .IsRequired();
                builder.Property(u => u.IdUnidade)
                    .IsRequired();
                builder.Property(u => u.EhEntrega)
                    .IsRequired()
                    .HasDefaultValue(false);

                builder.Property(u => u.Observacao)
                    .HasMaxLength(100);

                builder.Property(u => u.CanalPedido)
                    .IsRequired()
                    .HasColumnName("IdCanalPedido")
                    .HasMaxLength(100);
            }
        }
    }
}
