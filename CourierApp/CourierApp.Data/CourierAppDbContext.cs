using CourierApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Data
{
    public class CourierAppDbContext : DbContext
    {
        public CourierAppDbContext(DbContextOptions<CourierAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DefineTables(modelBuilder);

            SetRelationship(modelBuilder);
        }

        public DbSet<Courier> Couriers { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Review> Reviews { get; set; }

        private void DefineTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courier>().HasKey(c => c.Id);
            modelBuilder.Entity<Courier>().Property(c => c.FirstName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Courier>().Property(c => c.SecondName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Courier>().Property(c => c.PhoneNumber).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Courier>().Property(c => c.Email).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Courier>().Property(c => c.Password).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Package>().HasKey(p => p.Id);
            modelBuilder.Entity<Package>().Property(p => p.Address).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Package>().Property(p => p.CustomerEmail).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Package>().Property(p => p.Status).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Review>().HasKey(r => r.Id);
            modelBuilder.Entity<Review>().Property(r => r.Content).HasMaxLength(250);
            modelBuilder.Entity<Review>().Property(r => r.Mark).IsRequired();
            modelBuilder.Entity<Review>().Property(r => r.Author).HasMaxLength(50).IsRequired();

        }
        private void SetRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>()
                .HasOne<Courier>(p => p.Courier)
                .WithMany(c => c.Packages)
                .HasForeignKey(p => p.CourierId);

            modelBuilder.Entity<Review>()
                .HasOne<Courier>(r => r.Courier)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CourierId);
        }


    }
}
