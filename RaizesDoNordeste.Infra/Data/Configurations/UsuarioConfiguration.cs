using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            {
                builder.ToTable("Usuarios");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(u => u.Email)
                    .HasMaxLength(150);

                builder.Property(u => u.Senha)
                    .HasMaxLength(255);

                builder.Property(u => u.Ativo)
                    .HasDefaultValue(true);

                builder.HasIndex(u => u.Email)
                    .IsUnique();
            }
        }
    }
}
