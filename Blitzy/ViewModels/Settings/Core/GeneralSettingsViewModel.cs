using Blitzy.Models;
using Blitzy.Resources;
using System.Threading.Tasks;

namespace Blitzy.ViewModels.Settings.Core
{
	internal class GeneralSettingsViewModel : SettingsSectionViewModel
	{
		public GeneralSettingsViewModel( ITreeViewItemViewModel parent, ISettings settings ) : base( parent, settings, Strings.General )
		{
		}

		protected override Task OnSave()
		{
			return Task.CompletedTask;
		}
	}
}