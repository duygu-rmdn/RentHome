namespace RentHome.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RentHome.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }


        public DbSet<Property> Properties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<CloudImage> CloudImages { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Rental>()
                   .HasOne(r => r.Tenant)
                   .WithMany(t => t.Rentals)
                   .HasForeignKey(r => r.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

         /* builder.Entity<Rental>()
                   .HasOne(r => r.)
                   .WithMany(t => t.Rentals)
                   .HasForeignKey(r => r.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);*/

            builder.Entity<User>()
                   .HasMany(u => u.Rentals)
                   .WithOne(r => r.Tenant)
                   .HasForeignKey(r => r.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Property>()
                   .HasOne(h => h.Manager)
                   .WithMany(m => m.ManagedProperties)
                   .HasForeignKey(h => h.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                   .HasMany(u => u.ManagedProperties)
                   .WithOne(h => h.Manager)
                   .HasForeignKey(h => h.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
