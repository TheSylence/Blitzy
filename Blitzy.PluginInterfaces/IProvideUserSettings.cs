using System.Threading.Tasks;

namespace Blitzy.PluginInterfaces
{
	/// <summary>
	///     Implement this (and return an instance in <see cref="IPlugin.UserSettings" />) to provide a way for the user
	///     to edit settings for your plugin.
	/// </summary>
	public interface IProvideUserSettings
	{
		/// <summary>
		///     This is called when the user opens the settings section for your plugin.
		///     Use this to populate your properties with the current values.
		/// </summary>
		Task Load();

		/// <summary>
		///     This is called when the user edited settings and now wants to save them.
		/// </summary>
		Task Save();
	}
}