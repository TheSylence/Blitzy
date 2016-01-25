using Blitzy.ViewModels.Main;
using Blitzy.ViewModels.Settings;
using Ninject;

namespace Blitzy.ViewModels
{
	internal class ViewModelLocator
	{
		public ViewModelLocator()
		{
			Kernel = App.Kernel;
		}

		public IMainViewModel Main => Kernel.Get<IMainViewModel>();
		public ISettingsDialogViewModel SettingsDialog => Kernel.Get<ISettingsDialogViewModel>();

		private readonly IKernel Kernel;
	}
}