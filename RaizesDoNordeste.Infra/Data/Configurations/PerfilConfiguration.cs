using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            {
                builder.ToTable("Perfils");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.Nome)
                    .HasMaxLength(100)
                    .IsRequired();

                builder.HasData(
                    new Perfil { Id = 1, Nome = "Admin" },
                    new Perfil { Id = 2, Nome = "Gerente" },
                    new Perfil { Id = 3, Nome = "Cliente" }
                );
            }
        }
    }
}
