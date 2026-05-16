using GameStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Game> Games { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Nome).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
                entity.Property(u => u.SenhaHash).IsRequired();
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(g => g.Descricao).IsRequired().HasMaxLength(1000);
                entity.Property(g => g.Preco).HasColumnType("decimal(10,2)");
                entity.Property(g => g.UrlCapa).HasMaxLength(500);
                entity.Property(g => g.Categoria).HasMaxLength(50).HasDefaultValue("Ação");
            });
        }
    }
}
