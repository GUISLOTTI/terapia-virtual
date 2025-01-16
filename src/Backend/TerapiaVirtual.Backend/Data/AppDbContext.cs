using Microsoft.EntityFrameworkCore;
using TerapiaVirtual.Backend.Models;

namespace TerapiaVirtual.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // tabelas banco de dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Nome).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(64);
                entity.Property(u => u.Senha).IsRequired();

                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Sessao>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(s => s.DataHora).IsRequired();
                entity.Property(s => s.Descricao).HasMaxLength(500);

                entity.HasOne(s => s.Usuario)
                    .WithMany(u => u.Sessoes)
                    .HasForeignKey(s => s.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}