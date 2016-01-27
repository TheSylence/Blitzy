using Blitzy.PluginInterfaces;

namespace Blitzy.Models.Plugins
{
	class PluginHost : IPluginHost
	{
		public PluginHost( IDatabase database )
		{
			Database = database;
		}

		public IDatabase Database { get; }
	}
}