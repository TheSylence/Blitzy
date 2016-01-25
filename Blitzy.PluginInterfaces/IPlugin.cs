using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blitzy.PluginInterfaces.Commands;

namespace Blitzy.PluginInterfaces
{
	/// <summary>
	///     Implement this interface to provide a plugin for Blitzy.
	/// </summary>
	public interface IPlugin
	{
		/// <summary>
		///     Called when the plugin is being loaded.
		///     Use this to check for prequisites or to start any background activity if you plugin needs one.
		/// </summary>
		/// <param name="host">Interface to access data from the core application.</param>
		/// <remarks>
		///     To indicate a failre, throw an exception.
		///     They will be logged and the user will be informed that your plugin failed to load.
		/// </remarks>
		Task Load( IPluginHost host );

		/// <summary>
		///     Called when the plugin is being unloaded.
		///     Use this to shutdown any background activity if you started one during the <see cref="Load" /> call.
		/// </summary>
		/// <remarks>Exceptions that are thrown by this method will be logged and swallowed.</remarks>
		Task Unload();

		/// <summary>
		///     Gets the description of the plugin. This value is displayed to the user.
		/// </summary>
		string Description { get; }

		/// <summary>
		///     Gets the name of the plugin. This value is displayed to the user.
		/// </summary>
		string Name { get; }

		/// <summary>
		///     Used to access the settings of your plugin a user can edit.
		///     Return <c>null</c> if your plugin does not provide any settings.
		/// </summary>
		IProvideUserSettings UserSettings { get; }

		/// <summary>
		///     Gets the version of your plugin. It is recommended that you simply return the file version of your plugin.
		/// </summary>
		Version Version { get; }

		/// <summary>
		///     Gets a website where the user may get further information about your plugin.
		/// </summary>
		Uri Website { get; }

		/// <summary>
		/// Called to retrieve all top command nodes your plugin offers.
		/// </summary>
		/// <returns></returns>
		IEnumerable<ICommandNode> GetNodes();
	}
}