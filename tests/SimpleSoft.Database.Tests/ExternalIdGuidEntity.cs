using System;

namespace SimpleSoft.Database
{
    public class ExternalIdGuidEntity : Entity, IHaveExternalId
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; }
    }
}