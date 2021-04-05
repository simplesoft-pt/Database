using Microsoft.EntityFrameworkCore;

namespace SimpleSoft.Database.EFCore
{
    public class TestDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder
            .UseSqlite(TestSingletons.ConnectionString);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ExternalIdGuidEntity>(cfg =>
            {
                cfg.HasKey(e => e.Id);
                cfg.HasAlternateKey(e => e.ExternalId);

                cfg.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                cfg.Property(e => e.ExternalId)
                    .IsRequired();
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            builder.Entity<ExternalIdStringEntity>(cfg =>
            {
                cfg.HasKey(e => e.Id);
                cfg.HasAlternateKey(e => e.ExternalId);

                cfg.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                cfg.Property(e => e.ExternalId)
                    .IsRequired();
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            builder.Entity<IdLongEntity>(cfg =>
            {
                cfg.HasKey(e => e.Id);

                cfg.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            builder.Entity<IdStringEntity>(cfg =>
            {
                cfg.HasKey(e => e.Id);

                cfg.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(32);
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });
        }
    }
}
