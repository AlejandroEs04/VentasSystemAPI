using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Data
{
    public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetails> SaleDetails { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<ClaveProdServ_SAT> ClaveProdServ_SAT { get; set; }
        public DbSet<ClaveUnidad_SAT> ClaveUnidad_SAT { get; set; }
        public DbSet<Impuesto_SAT> Impuesto_SAT { get; set; }
        public DbSet<RegimenFiscal> RegimenFiscal { get; set; }
        public DbSet<ObjetoImp_SAT> ObjetoImp_SAT { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRole> RoleMenus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<VW_ProductsSellers> ProductsSellers { get; set; }
        public DbSet<SaleDocumentType> DocumentTypes { get; set; }
        public DbSet<SaleReportResponseDto> SaleReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí puedes configurar relaciones, llaves, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
