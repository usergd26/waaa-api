using Microsoft.EntityFrameworkCore;
using Waaa.Domain.Entities;

namespace Waaa.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<WebinarRegistration> WebinarRegistrations { get; set; }

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

            modelBuilder.Entity<WebinarRegistration>(entity =>
            {
                entity.HasKey(wr => wr.Id);
                entity.Property(wr => wr.Id)
                      .ValueGeneratedOnAdd();
                entity.Property(wr => wr.UserId)
                      .IsRequired();
                entity.Property(wr => wr.WebinarId)
                      .IsRequired();
                entity.Property(wr => wr.RegistrationDate)
                      .IsRequired();
                entity.HasOne(wr => wr.User)
                      .WithMany()
                      .HasForeignKey(wr => wr.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
