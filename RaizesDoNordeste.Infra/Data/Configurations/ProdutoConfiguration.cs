using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            {
                builder.ToTable("Produtos");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.Nome)
                    .HasMaxLength(100)
                    .IsRequired();
                builder.Property(u => u.Preco)
                    .IsRequired();
                
                builder.HasIndex(e => new { e.Nome, e.Preco })
                    .IsUnique();
            }
        }
    }
}
