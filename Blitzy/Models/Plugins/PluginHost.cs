using Blitzy.PluginInterfaces;

namespace Blitzy.Models.Plugins
{
	class PluginHost : IPluginHost
	{
		public IDatabase Database { get; }
	}
}