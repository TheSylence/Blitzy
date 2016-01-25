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

		public ISettingsDialogViewModel SettingsDialog => Kernel.Get<ISettingsDialogViewModel>();

		private readonly IKernel Kernel;
	}
}