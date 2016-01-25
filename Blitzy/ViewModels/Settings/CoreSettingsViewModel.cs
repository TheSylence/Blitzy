using Blitzy.Resources;

namespace Blitzy.ViewModels.Settings
{
	internal class CoreSettingsViewModel : TreeViewItemViewModel
	{
		public CoreSettingsViewModel() : base( null, "Blitzy" )
		{
			Children.Add( new VisualSettingsViewModel( this ) );
			Children.Add( new BehaviorSettingsViewModel( this ) );
			Children.Add( new UpdateSettingsViewModel( this ) );
		}
	}

	internal class VisualSettingsViewModel : TreeViewItemViewModel
	{
		public VisualSettingsViewModel( ITreeViewItemViewModel parent ) : base( parent, Strings.Visual )
		{
		}
	}

	internal class UpdateSettingsViewModel : TreeViewItemViewModel
	{
		public UpdateSettingsViewModel( ITreeViewItemViewModel parent ) : base( parent, Strings.Updates )
		{
		}
	}

	internal class BehaviorSettingsViewModel : TreeViewItemViewModel
	{
		public BehaviorSettingsViewModel( ITreeViewItemViewModel parent ) : base( parent, Strings.Behavior )
		{
		}
	}
}