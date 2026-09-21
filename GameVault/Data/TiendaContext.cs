using Microsoft.EntityFrameworkCore;


using GameVault.Models;

namespace GameVault.Data
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
    }
}
