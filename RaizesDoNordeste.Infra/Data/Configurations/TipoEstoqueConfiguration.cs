using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class TipoEstoqueConfiguration : IEntityTypeConfiguration<TipoEstoque>
    {
        public void Configure(EntityTypeBuilder<TipoEstoque> builder)
        {
            {
                builder.ToTable("TipoEstoques");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.Nome)
                    .HasMaxLength(100)
                    .IsRequired();
            }
        }
    }
}
