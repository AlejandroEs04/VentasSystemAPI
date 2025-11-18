using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Data
{
    public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
    {
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí puedes configurar relaciones, llaves, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
