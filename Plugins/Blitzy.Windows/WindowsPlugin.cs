using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Blitzy.PluginInterfaces;
using Blitzy.PluginInterfaces.Commands;

namespace Blitzy.Windows
{
	public class WindowsPlugin : IPlugin
	{
		/// <summary>
		///     Called to retrieve all top command nodes your plugin offers.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ICommandNode> GetNodes()
		{
			yield return new WindowsCommand( CommandType.Shutdown, Host.Database );
			yield return new WindowsCommand( CommandType.Restart, Host.Database );
			yield return new WindowsCommand( CommandType.Logoff, Host.Database );
		}

		/// <summary>
		///     Called when the plugin is being loaded.
		///     Use this to check for prequisites or to start any background activity if you plugin needs one.
		/// </summary>
		/// <param name="host">Interface to access data from the core application.</param>
		/// <remarks>
		///     To indicate a failre, throw an exception.
		///     They will be logged and the user will be informed that your plugin failed to load.
		/// </remarks>
		public Task Load( IPluginHost host )
		{
			Host = host;
			UserSettings = new WindowsSettings( host.Database );

			return Task.CompletedTask;
		}

		/// <summary>
		///     Called when the plugin is being unloaded.
		///     Use this to shutdown any background activity if you started one during the <see cref="Load" /> call.
		/// </summary>
		/// <remarks>Exceptions that are thrown by this method will be logged and swallowed.</remarks>
		public Task Unload()
		{
			return Task.CompletedTask;
		}

		/// <summary>
		///     Gets the description of the plugin. This value is displayed to the user.
		/// </summary>
		public string Description => "Control Windows from Blitzy (Shutdown, Restart, Logoff, etc.)";

		/// <summary>
		///     Gets the name of the plugin. This value is displayed to the user.
		/// </summary>
		public string Name => "Windows";

		/// <summary>
		///     Used to access the settings of your plugin a user can edit.
		///     Return <c>null</c> if your plugin does not provide any settings.
		/// </summary>
		public IProvideUserSettings UserSettings { get; private set; }

		/// <summary>
		///     Gets the version of your plugin. It is recommended that you simply return the file version of your plugin.
		/// </summary>
		public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

		/// <summary>
		///     Gets a website where the user may get further information about your plugin.
		/// </summary>
		public Uri Website => new Uri( "http://btbsoft.org" );

		private IPluginHost Host;
	}
}