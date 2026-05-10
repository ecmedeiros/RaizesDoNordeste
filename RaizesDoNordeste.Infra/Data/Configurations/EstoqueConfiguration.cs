using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            {
                builder.ToTable("Estoques");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.IdUnidade)
                    .IsRequired();

                builder.Property(u => u.IdProduto)
                    .IsRequired();
                
                builder.Property(u => u.Quantidade)
                    .IsRequired();

                builder.HasIndex(e => new { e.IdUnidade, e.IdProduto})
                    .IsUnique();
            }
        }
    }
}
