namespace SimpleSoft.Database
{
    /// <summary>
	/// Interface representing the unit of work factory
	/// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Create a scoped unit of work
		/// </summary>
		/// <returns>An <see cref="IUnitOfWorkScoped"/></returns>
        IUnitOfWorkScoped Create();
    }
}
