using APICRUDTESTING.Models;
using Microsoft.EntityFrameworkCore;

namespace APICRUDTESTING.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("server=INBOOK_X1;database=product_management;trusted_connection=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Menentukan Username sebagai Primary Key menggunakan Fluent API
            modelBuilder.Entity<User>()
                .HasKey(u => u.Username); // Tentukan Username sebagai primary key
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
