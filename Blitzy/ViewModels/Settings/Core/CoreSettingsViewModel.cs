using Blitzy.Models;

namespace Blitzy.ViewModels.Settings.Core
{
	internal class CoreSettingsViewModel : TreeViewItemViewModel
	{
		public CoreSettingsViewModel( ISettings settings ) : base( null, "Blitzy" )
		{
			Children.Add( new GeneralSettingsViewModel( this, settings ) );
			Children.Add( new VisualSettingsViewModel( this, settings ) );
			Children.Add( new BehaviorSettingsViewModel( this, settings ) );
			Children.Add( new UpdateSettingsViewModel( this, settings ) );
		}
	}
}