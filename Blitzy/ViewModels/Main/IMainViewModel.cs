using System.Windows.Input;

namespace Blitzy.ViewModels.Main
{
	internal interface IMainViewModel
	{
		ICommandController CommandController { get; }
		IInputProcessor InputProcessor { get; }
		string InputText { get; set; }
		ICommand SettingsCommand { get; }
	}
}