using DonaMenina.Entities;
using Microsoft.EntityFrameworkCore;

namespace DonaMenina.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<MergeClass> MergeClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);

            modelBuilder.Entity<Worker>().HasKey(w => w.WorkerId);

            modelBuilder.Entity<Sale>().HasKey(s => s.SaleId);

            modelBuilder.Entity<MergeClass>()
        .HasKey(mc => new { mc.SaleId, mc.WorkerID, mc.ProductId });

            modelBuilder.Entity<MergeClass>()
                .HasOne(mc => mc.Sale)
                .WithMany()
                .HasForeignKey(mc => mc.SaleId);

            modelBuilder.Entity<MergeClass>()
                .HasOne(mc => mc.Worker)
                .WithMany()
                .HasForeignKey(mc => mc.WorkerID);

            modelBuilder.Entity<MergeClass>()
                .HasOne(mc => mc.Product)
                .WithMany()
                .HasForeignKey(mc => mc.ProductId);
        }
    }
}
