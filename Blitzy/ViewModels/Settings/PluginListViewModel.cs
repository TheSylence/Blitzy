using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Blitzy.Models;
using Blitzy.Models.Plugins;
using Blitzy.PluginInterfaces;
using Blitzy.Resources;

namespace Blitzy.ViewModels.Settings
{
	internal class PluginListViewModel : TreeViewItemViewModel, ILoadCallback
	{
		public PluginListViewModel( ISettings settings, IPluginContainer pluginContainer ) : base( null, Strings.Plugins, true )
		{
			Container = pluginContainer;
		}

		public async Task OnLoad( object data )
		{
			foreach( var child in Children.OfType<ILoadCallback>() )
			{
				await child.OnLoad( data );
			}
		}

		protected override void LoadChildren()
		{
			var withSettings = Container.LoadedPlugins.Where( p => p.UserSettings != null ).ToArray();
			var others = Container.LoadedPlugins.Except( withSettings );

			foreach( var plugin in withSettings )
			{
				Children.Add( new PluginSettingsViewModel( this, plugin ) );
			}
		}

		private readonly IPluginContainer Container;
	}

	internal class PluginSettingsViewModel : TreeViewItemViewModel, ILoadCallback
	{
		public PluginSettingsViewModel( ITreeViewItemViewModel parent, IPlugin plugin ) : base( parent, plugin.Name )
		{
			UserSettings = plugin.UserSettings;

			Content = UserSettings;
		}

		public async Task OnLoad( object data )
		{
			await UserSettings.Load();
		}

		public object Content { get; }
		public bool HasViewImplementation => Content is FrameworkElement;
		private readonly IProvideUserSettings UserSettings;
	}
}