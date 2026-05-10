using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class PontosUsuarioConfiguration : IEntityTypeConfiguration<PontosUsuario>
    {
        public void Configure(EntityTypeBuilder<PontosUsuario> builder)
        {
            {
                builder.ToTable("PontosUsuarios");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.IdUsuario)
                    .IsRequired();

                builder.Property(u => u.Quantidade)
                    .IsRequired();
                
                builder.Property(u => u.Validade)
                    .IsRequired();
                
                builder.HasIndex(u => u.IdUsuario)
                    .IsUnique();
            }
        }
    }
}
