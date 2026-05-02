
using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entidades;
using System.Reflection.Metadata.Ecma335;

namespace RaizesDoNordeste.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Unidade> Unidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Unidade>()
                .HasIndex(u => u.CNPJ)
                .IsUnique();
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntidadeBase>())
            {
                if (entry.State == EntityState.Modified)
                    entry.Entity.DataAtualizacao = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
