using Microsoft.EntityFrameworkCore;
using GameVault.Models;

namespace GameVault.Data
{
    public class TiendaDbContext : DbContext
    {
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
