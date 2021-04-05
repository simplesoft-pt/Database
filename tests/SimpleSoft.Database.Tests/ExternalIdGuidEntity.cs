using System;

namespace SimpleSoft.Database
{
    public class ExternalIdGuidEntity : Entity<long>, IHaveExternalId
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; }
    }
}