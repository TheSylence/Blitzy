namespace Blitzy.PluginInterfaces
{
	/// <summary>
	/// Interface for accessing functionality from the core inside a plugin.
	/// </summary>
	public interface IPluginHost
	{
		/// <summary>
		/// Provides access to the global configuration database.
		/// </summary>
		IDatabase Database { get; }
	}
}