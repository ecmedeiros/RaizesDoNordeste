using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class MovimentacaoEstoqueConfiguration : IEntityTypeConfiguration<MovimentacaoEstoque>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoEstoque> builder)
        {
            {
                builder.ToTable("MovimentacaoEstoques");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.IdEstoque)
                    .IsRequired();

                builder.Property(u => u.IdTipoEstoque)
                    .IsRequired();
                
                builder.Property(u => u.Quantidade)
                    .IsRequired();
                
                builder.Property(u => u.Motivo)
                    .HasMaxLength(200)
                    .IsRequired();
                
                builder.Property(u => u.IdUsuario)
                    .IsRequired();

                builder.HasOne(i => i.Estoque)
                    .WithMany(p => p.Movimentacoes)
                    .HasForeignKey(i => i.IdEstoque)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(i => i.Usuario)
                    .WithMany()
                    .HasForeignKey(i => i.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
