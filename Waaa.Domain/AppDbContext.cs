﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Waaa.Domain.Entities;

namespace Waaa.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<WebinarRegistration> WebinarRegistrations { get; set; }
        public DbSet<BluePrint> BluePrints { get; set; }
        public DbSet<Webinar> Webinars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(entity =>
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

                entity.HasIndex(u => u.Phone)
                       .IsUnique();

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasIndex(u => u.Email)
                       .IsUnique();
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
                entity.HasOne(wr => wr.Webinar)
                      .WithMany()
                      .HasForeignKey(wr => wr.WebinarId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(wr => wr.RegistrationDate)
                      .IsRequired();

                entity.HasOne(wr => wr.AppUser)
                      .WithMany()
                      .HasForeignKey(wr => wr.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BluePrint>(entity =>
            {
                entity.HasKey(wr => wr.Id);

                entity.Property(wr => wr.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(wr => wr.UserId)
                      .IsRequired();

                entity.Property(wr => wr.RegistrationDate)
                      .IsRequired();

                entity.HasOne(wr => wr.AppUser)
                      .WithMany()
                      .HasForeignKey(wr => wr.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Webinar>(entity =>
            {
                entity.HasKey(wr => wr.Id);

                entity.Property(wr => wr.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Code)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.HasIndex(u => u.Code)
                      .IsUnique();
            });
        }

    }
}
