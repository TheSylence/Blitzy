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
			ConfirmShutdown = await Database.Get<bool>( "Blitzy.Windows.ConfirmShutdown" );
			ConfirmRestart = await Database.Get<bool>( "Blitzy.Windows.ConfirmRestart" );
			ConfirmLogoff = await Database.Get<bool>( "Blitzy.Windows.ConfirmLogoff" );
		}
		
		public async Task Save()
		{
			await Database.Set( "Blitzy.Windows.ConfirmShutdown", ConfirmShutdown );
			await Database.Set( "Blitzy.Windows.ConfirmRestart", ConfirmRestart );
			await Database.Set( "Blitzy.Windows.ConfirmLogoff", ConfirmLogoff );
		}

		public bool ConfirmLogoff { get; set; }
		public bool ConfirmRestart { get; set; }
		public bool ConfirmShutdown { get; set; }

		private readonly IDatabase Database;
	}
}