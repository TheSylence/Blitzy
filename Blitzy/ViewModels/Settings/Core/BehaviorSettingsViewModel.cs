using Blitzy.Models;
using Blitzy.Resources;

namespace Blitzy.ViewModels.Settings.Core
{
	internal class BehaviorSettingsViewModel : TreeViewItemViewModel
	{
		public BehaviorSettingsViewModel( ITreeViewItemViewModel parent, ISettings settings ) : base( parent, Strings.Behavior )
		{
		}
	}
}