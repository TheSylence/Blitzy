using System.Threading.Tasks;
using Blitzy.PluginInterfaces;

namespace Blitzy.Windows
{
	internal class WindowsSettings : IProvideUserSettings
	{
		public WindowsSettings( IDatabase database )
		{
			Database = database;
		}

		public async Task Load()
		{
			if( !await Database.KeyExists( ShutdownKey ) )
			{
				await Database.Set( ShutdownKey, true );
			}

			if( !await Database.KeyExists( RestartKey ) )
			{
				await Database.Set( RestartKey, true );
			}

			if( !await Database.KeyExists( LogoffKey ) )
			{
				await Database.Set( LogoffKey, true );
			}

			ConfirmShutdown = await Database.Get<bool>( ShutdownKey );
			ConfirmRestart = await Database.Get<bool>( RestartKey );
			ConfirmLogoff = await Database.Get<bool>( LogoffKey );
		}

		public async Task Save()
		{
			await Database.Set( ShutdownKey, ConfirmShutdown );
			await Database.Set( RestartKey, ConfirmRestart );
			await Database.Set( LogoffKey, ConfirmLogoff );
		}

		public bool ConfirmLogoff { get; set; }
		public bool ConfirmRestart { get; set; }
		public bool ConfirmShutdown { get; set; }
		internal const string LogoffKey = "Blitzy.Windows.ConfirmLogoff";
		internal const string RestartKey = "Blitzy.Windows.ConfirmRestart";
		internal const string ShutdownKey = "Blitzy.Windows.ConfirmShutdown";
		private readonly IDatabase Database;
	}
}