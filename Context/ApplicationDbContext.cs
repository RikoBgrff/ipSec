namespace ipSec.Context
{
    using ipSec.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<TrapLog> TrapLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrapLog>(entity =>
            {
                entity.ToTable("TrapLogs");

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.Property(e => e.UserAgent).HasMaxLength(500);
                entity.Property(e => e.Platform).HasMaxLength(100);
                entity.Property(e => e.Language).HasMaxLength(50);
                entity.Property(e => e.Time).HasMaxLength(100);
                entity.Property(e => e.ScreenResolution).HasMaxLength(50);
                entity.Property(e => e.Timezone).HasMaxLength(100);
                entity.Property(e => e.Referrer).HasMaxLength(300);
                entity.Property(e => e.DeviceMemory).HasMaxLength(50);
                entity.Property(e => e.HardwareConcurrency).HasMaxLength(50);
                entity.Property(e => e.IPAddress).HasMaxLength(50);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.Region).HasMaxLength(100);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.ISP).HasMaxLength(200);
            });
        }
    }
}