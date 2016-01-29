using Blitzy.Models;
using Blitzy.Utilities;

namespace Blitzy.ViewModels.Settings.Core
{
	internal class CoreSettingsViewModel : TreeViewItemViewModel
	{
		public CoreSettingsViewModel( ISettings settings, IAppThemes appThemes ) : base( null, "Blitzy" )
		{
			Children.Add( new GeneralSettingsViewModel( this, settings ) );
			Children.Add( new VisualSettingsViewModel( this, settings, appThemes ) );
			Children.Add( new BehaviorSettingsViewModel( this, settings ) );
			Children.Add( new UpdateSettingsViewModel( this, settings ) );
		}
	}
}