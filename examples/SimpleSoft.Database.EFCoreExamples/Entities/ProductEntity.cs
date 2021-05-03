using System;

namespace SimpleSoft.Database.EFCoreExamples.Entities
{
    public class ProductEntity : Entity<long>, IHaveExternalId
    {
        public Guid ExternalId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}