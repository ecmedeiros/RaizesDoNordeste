using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class ItensPedidoConfiguration : IEntityTypeConfiguration<ItensPedido>
    {
        public void Configure(EntityTypeBuilder<ItensPedido> builder)
        {
            {
                builder.ToTable("ItensPedidos");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.PrecoUnitario)
                    .IsRequired();
                builder.Property(u => u.Quantidade)
                    .IsRequired();
                builder.Property(u => u.IdProduto)
                    .IsRequired();
                builder.Property(u => u.IdPedido)
                    .IsRequired();

                builder.HasIndex(e => new { e.IdProduto, e.IdPedido })
                    .IsUnique();
            }
        }
    }
}
