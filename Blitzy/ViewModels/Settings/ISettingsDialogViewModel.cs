using System.Collections.Generic;
using System.Windows.Input;

namespace Blitzy.ViewModels.Settings
{
	internal interface ISettingsDialogViewModel : IWindowController, ILoadCallback
	{
		ICommand CancelCommand { get; }
		bool HasUnsavedChanges { get; }
		ICommand SaveCommand { get; }
		ITreeViewItemViewModel SelectedItem { get; }
		ICollection<ITreeViewItemViewModel> TopLevelItems { get; }
		int UnsavedChanges { get; }
	}
}