using Blitzy.Views;
using System.Windows;

namespace Blitzy.Services
{
	interface ISettingsDialogService
	{
		void Show();
	}

	internal class SettingsDialogService : ISettingsDialogService
	{
		public void Show()
		{
			var dlg = new SettingsDialog
			{
				Owner = Application.Current.MainWindow
			};
			dlg.ShowDialog();
		}
	}
}