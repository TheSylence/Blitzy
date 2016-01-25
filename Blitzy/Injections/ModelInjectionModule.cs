using Blitzy.Models.Plugins;
using Blitzy.PluginInterfaces;
using Ninject.Modules;

namespace Blitzy.Injections
{
	internal class ModelInjectionModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IPluginContainer>().To<PluginContainer>().InSingletonScope();
			Bind<IPluginHost>().To<PluginHost>().InSingletonScope();
		}
	}
}