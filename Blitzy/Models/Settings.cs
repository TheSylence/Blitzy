using System.Threading.Tasks;
using Blitzy.PluginInterfaces;

namespace Blitzy.Models
{
	internal interface ISettings
	{
		Task Load();

		Task Save();

		/// <summary>
		///     Gets or sets the accent of the theme that will be used.
		/// </summary>
		string Accent { get; set; }

		/// <summary>
		///     Determines whether checking for updates is enabled.
		/// </summary>
		bool CheckForUpdates { get; set; }

		/// <summary>
		///     Determines whether the window will close after a command has been executed.
		/// </summary>
		bool CloseAfterExecution { get; set; }

		/// <summary>
		///     Determines whether the window will close when pressing the escape key.
		/// </summary>
		bool CloseOnEscape { get; set; }

		/// <summary>
		///     Determines whether the window will close close when it looses the input focus.
		/// </summary>
		bool CloseOnFocusLost { get; set; }

		IDatabase Database { get; }

		/// <summary>
		///     Hotkey that is used to bring up the window
		/// </summary>
		string HotKey { get; set; }

		/// <summary>
		///     Modifier that is used to bring the window
		/// </summary>
		string HotKeyModifier { get; set; }

		/// <summary>
		///     Determines whether the command text box will keep its content after the window has been closed.
		/// </summary>
		bool KeepInputContent { get; set; }

		/// <summary>
		///     Gets or sets the maximum number items that are shown in the command list.
		/// </summary>
		int MaxMatchingItems { get; set; }

		/// <summary>
		///     Determines whether preview releases are included in update search.
		/// </summary>
		bool PreviewUpdates { get; set; }

		/// <summary>
		///     Determines whether the command list can be scrolled infinitive.
		/// </summary>
		bool ScrollThroughCommandList { get; set; }

		/// <summary>
		///     Determines whether the application's tray icon will be displayed.
		/// </summary>
		bool ShowTrayIcon { get; set; }

		/// <summary>
		/// <c>true</c> if plugin commands are directly injected into the root of the command tree.
		/// </summary>
		/// <remarks>
		/// true:
		///		root -> pluginCommand 1
		///			 \
		///				pluginCommand 2
		///
		/// false:
		///		root -> plugin -> pluginCommand 1
		///						\
		///							pluginCommand 2
		/// </remarks>
		bool StoreCommandsInRoot { get; set; }

		/// <summary>
		///     Gets or sets the theme that will be used.
		/// </summary>
		string Theme { get; set; }
	}

	internal class Settings : ISettings
	{
		public Settings( IDatabase db )
		{
			Database = db;
		}

		public async Task Load()
		{
			Accent = await Database.Get<string>( "Blitzy.Settings.Accent" );
			CheckForUpdates = await Database.Get<bool>( "Blitzy.Settings.CheckForUpdates" );
			PreviewUpdates = await Database.Get<bool>( "Blitzy.Settings.PreviewUpdates" );
			CloseAfterExecution = await Database.Get<bool>( "Blitzy.Settings.CloseAfterExecution" );
			CloseOnEscape = await Database.Get<bool>( "Blitzy.Settings.CloseOnEscape" );
			CloseOnFocusLost = await Database.Get<bool>( "Blitzy.Settings.CloseOnFocusLost" );
			HotKey = await Database.Get<string>( "Blitzy.Settings.HotKey" );
			HotKeyModifier = await Database.Get<string>( "Blitzy.Settings.HotKeyModifier" );
			KeepInputContent = await Database.Get<bool>( "Blitzy.Settings.KeepInputContent" );
			MaxMatchingItems = await Database.Get<int>( "Blitzy.Settings.MaxMatchingItems" );
			ScrollThroughCommandList = await Database.Get<bool>( "Blitzy.Settings.ScrollThroughCommandList" );
			ShowTrayIcon = await Database.Get<bool>( "Blitzy.Settings.ShowTrayIcon" );
			Theme = await Database.Get<string>( "Blitzy.Settings.Theme" );
			StoreCommandsInRoot = await Database.Get<bool>( "Blitzy.Settings.StoreCommandsInRoot" );
		}

		public async Task Save()
		{
			await Database.Set( "Blitzy.Settings.Accent", Accent );
			await Database.Set( "Blitzy.Settings.CheckForUpdates", CheckForUpdates );
			await Database.Set( "Blitzy.Settings.PreviewUpdates", PreviewUpdates );
			await Database.Set( "Blitzy.Settings.CloseAfterExecution", CloseAfterExecution );
			await Database.Set( "Blitzy.Settings.CloseOnEscape", CloseOnEscape );
			await Database.Set( "Blitzy.Settings.CloseOnFocusLost", CloseOnFocusLost );
			await Database.Set( "Blitzy.Settings.HotKey", HotKey );
			await Database.Set( "Blitzy.Settings.HotKeyModifier", HotKeyModifier );
			await Database.Set( "Blitzy.Settings.KeepInputContent", KeepInputContent );
			await Database.Set( "Blitzy.Settings.MaxMatchingItems", MaxMatchingItems );
			await Database.Set( "Blitzy.Settings.ScrollThroughCommandList", ScrollThroughCommandList );
			await Database.Set( "Blitzy.Settings.ShowTrayIcon", ShowTrayIcon );
			await Database.Set( "Blitzy.Settings.Theme", Theme );
			await Database.Set( "Blitzy.Settings.StoreCommandsInRoot", StoreCommandsInRoot );
		}

		public string Accent { get; set; }
		public bool CheckForUpdates { get; set; }
		public bool CloseAfterExecution { get; set; }
		public bool CloseOnEscape { get; set; }
		public bool CloseOnFocusLost { get; set; }
		public IDatabase Database { get; }
		public string HotKey { get; set; }
		public string HotKeyModifier { get; set; }
		public bool KeepInputContent { get; set; }
		public int MaxMatchingItems { get; set; }
		public bool PreviewUpdates { get; set; }
		public bool ScrollThroughCommandList { get; set; }
		public bool ShowTrayIcon { get; set; }
		public bool StoreCommandsInRoot { get; set; }
		public string Theme { get; set; }
	}
}