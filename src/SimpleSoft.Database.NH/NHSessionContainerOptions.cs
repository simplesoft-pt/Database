namespace SimpleSoft.Database
{
    /// <summary>
    /// Options for <see cref="NHSessionContainer"/>.
    /// </summary>
    public class NHSessionContainerOptions
    {
        /// <summary>
        /// Should changes be flushed into the database after executing and handler?
        /// Defaults to 'true'.
        /// </summary>
        public bool AutoFlush { get; set; } = true;

        /// <summary>
        /// Should tracking be disabled when fetching entities from the database?
        /// Defaults to 'true'.
        /// </summary>
        public bool NoTracking { get; set; } = true;
    }
}