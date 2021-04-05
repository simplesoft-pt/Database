namespace SimpleSoft.Database
{
    /// <summary>
    /// Options for <see cref="EFCoreContextContainer"/>.
    /// </summary>
    public class EFCoreContextContainerOptions
    {
        /// <summary>
        /// Should changes be saved into the database after executing and handler?
        /// Defaults to 'true'.
        /// </summary>
        public bool AutoSaveChanges { get; set; } = true;

        /// <summary>
        /// Should tracking be disabled when fetching entities from the database?
        /// Defaults to 'true'.
        /// </summary>
        public bool NoTracking { get; set; } = true;
    }
}