using Microsoft.EntityFrameworkCore;
using SistemaGestao.Models;

namespace SistemaGestao.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Hambúrguer", Preco = 25.00m, Tipo = TipoProduto.Prato },
                new Produto { Id = 2, Nome = "Refrigerante", Preco = 7.00m, Tipo = TipoProduto.Bebida },
                new Produto { Id = 3, Nome = "Batata Frita", Preco = 10.00m, Tipo = TipoProduto.Prato },
                new Produto { Id = 4, Nome = "Suco Natural", Preco = 8.50m, Tipo = TipoProduto.Bebida }
            );
        }
    }
}