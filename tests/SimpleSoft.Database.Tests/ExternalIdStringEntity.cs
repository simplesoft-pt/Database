namespace SimpleSoft.Database
{
    public class ExternalIdStringEntity : Entity<long>, IHaveExternalId<string>
    {
        public string ExternalId { get; set; }

        public string Name { get; set; }
    }
}