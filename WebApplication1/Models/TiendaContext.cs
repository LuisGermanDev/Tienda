using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class TiendaContext:DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext>options)
            : base(options) 
        {

        }
        public DbSet<Marca>Marcas { get; set; }
        public DbSet<Producto>Productos { get; set; }
    }
}
