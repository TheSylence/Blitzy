using Blitzy.Models;
using Blitzy.Models.Plugins;
using Blitzy.PluginInterfaces;
using Blitzy.Resources;

namespace Blitzy.ViewModels.Settings
{
	internal class PluginListViewModel : TreeViewItemViewModel
	{
		public PluginListViewModel( ISettings settings, IPluginContainer pluginContainer ) : base( null, Strings.Plugins )
		{
			Container = pluginContainer;
		}

		protected override void LoadChildren()
		{
			foreach( var plugin in Container.LoadedPlugins )
			{
				Children.Add( new PluginSettingsViewModel( this, plugin ) );
			}
		}

		private readonly IPluginContainer Container;
	}

	internal class PluginSettingsViewModel : TreeViewItemViewModel
	{
		public PluginSettingsViewModel( ITreeViewItemViewModel parent, IPlugin plugin ) : base( parent, plugin.Name )
		{
		}
	}
}