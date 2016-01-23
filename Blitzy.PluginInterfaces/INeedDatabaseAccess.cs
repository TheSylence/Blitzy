namespace Blitzy.PluginInterfaces
{
	/// <summary>
	/// Implement this if your plugin needs access to the global configuration database.
	/// </summary>
	public interface INeedDatabaseAccess
	{
		/// <summary>
		/// Gives you access to the global database.
		/// </summary>
		/// <param name="database">The database you can use to store data.</param>
		void SetDatabase( IDatabase database );
	}
}