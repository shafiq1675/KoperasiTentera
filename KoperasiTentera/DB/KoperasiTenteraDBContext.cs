
using KoperasiTentera.Models;
using Microsoft.EntityFrameworkCore;

namespace KoperasiTentera.DB
{
    public class KoperasiTenteraDBContext : DbContext
    {
        public KoperasiTenteraDBContext(DbContextOptions<KoperasiTenteraDBContext> options)
       : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.Id);

                // Indexes
                entity.HasIndex(e => e.ICNNumber).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.MobileNumber).IsUnique();

                // Property configurations
                entity.Property(e => e.ICNNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PinNumber)
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAddOrUpdate();

                // Default values for booleans
                entity.Property(e => e.IsMobileVerified)
                    .HasDefaultValue(false);

                entity.Property(e => e.IsEmailVerified)
                    .HasDefaultValue(false);

                entity.Property(e => e.HasAcceptedPolicy)
                    .HasDefaultValue(false);
            });

        }
    }
}
