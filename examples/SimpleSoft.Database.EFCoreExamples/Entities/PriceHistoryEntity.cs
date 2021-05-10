using System;

namespace SimpleSoft.Database.EFCoreExamples.Entities
{
    public class PriceHistoryEntity : Entity<long>
    {
        public long ProductId { get; set; }
        public decimal Value { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}