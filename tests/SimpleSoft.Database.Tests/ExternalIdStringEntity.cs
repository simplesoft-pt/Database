namespace SimpleSoft.Database
{
    public class ExternalIdStringEntity : Entity, IHaveExternalId<string>
    {
        public string ExternalId { get; set; }

        public string Name { get; set; }
    }
}