using Microsoft.EntityFrameworkCore;
using Waaa.Domain.Entities;

namespace Waaa.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Phone)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

            });
        }

    }
}
