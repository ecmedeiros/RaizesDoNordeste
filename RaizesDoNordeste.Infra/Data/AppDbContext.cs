
using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entidades;
using System.Reflection.Metadata.Ecma335;

namespace RaizesDoNordeste.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuario { get; set; } 
        public DbSet<PontosUsuario> PontosUsuario { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItensPedido> ItensPedido { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<MovimentacaoEstoque> MovimentacaoEstoques { get; set; }
        public DbSet<StatusPagamento> StatusPagamentos { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<UnidadeConfiguration> CanalPedido { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<TipoEstoque> TipoEstoque { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Unidade>()
                .HasIndex(u => u.CNPJ)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
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
