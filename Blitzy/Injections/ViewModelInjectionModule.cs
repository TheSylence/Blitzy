using Blitzy.ViewModels.Settings;
using Ninject.Modules;

namespace Blitzy.Injections
{
	internal class ViewModelInjectionModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISettingsDialogViewModel>().To<SettingsDialogViewModel>();
		}
	}
}