using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaizesDoNordeste.Domain.Entidades;

namespace RaizesDoNordeste.Infra.Data.Configurations
{
    public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            {
                builder.ToTable("Unidades");

                builder.HasKey(u => u.Id);

                builder.Property(u => u.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(u => u.CNPJ)
                    .IsRequired()
                    .HasMaxLength(14);

                builder.Property(u => u.Email)
                    .HasMaxLength(150);

                builder.Property(u => u.Telefone)
                    .HasMaxLength(20);

                builder.Property(u => u.Logradouro)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(u => u.Numero)
                    .IsRequired()
                    .HasMaxLength(10);

                builder.Property(u => u.Complemento)
                    .HasMaxLength(100);

                builder.Property(u => u.Bairro)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(u => u.Cidade)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(u => u.UF)
                    .IsRequired()
                    .HasMaxLength(2);

                builder.Property(u => u.CEP)
                    .IsRequired()
                    .HasMaxLength(8);

                builder.HasIndex(u => u.CNPJ)
                    .IsUnique();
            }
        }
    }
}
