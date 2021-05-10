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
    }
}