using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleSoft.Database.EFCoreExamples.Entities
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductEntity>(cfg =>
            {
                cfg.MapPrimaryKey();
                cfg.MapExternalId();
                cfg.HasIndex(e => e.Code).IsUnique();

                cfg.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8);
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            builder.Entity<PriceHistoryEntity>(cfg =>
            {
                cfg.MapPrimaryKey();
                cfg.HasIndex(e => e.CreatedOn);

                cfg.HasOne<ProductEntity>()
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
                cfg.Property(e => e.Value)
                    .IsRequired();
                cfg.Property(e => e.CreatedOn)
                    .IsRequired();
            });
        }
    }
}