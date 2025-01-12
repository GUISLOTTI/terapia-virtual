using Microsoft.EntityFrameworkCore;
using TerapiaVirtual.Backend.Models;
namespace TerapiaVirtual.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        // tabelas banco de dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
    }
}