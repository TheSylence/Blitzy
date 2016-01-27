using Blitzy.Models;
using Blitzy.Resources;

namespace Blitzy.ViewModels.Settings.Core
{
	class GeneralSettingsViewModel : TreeViewItemViewModel
	{
		public GeneralSettingsViewModel( ITreeViewItemViewModel parent, ISettings settings ) : base( parent, Strings.General )
		{
		}
	}
}