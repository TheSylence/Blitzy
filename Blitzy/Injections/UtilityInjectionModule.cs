using Blitzy.Utilities;
using Ninject.Modules;

namespace Blitzy.Injections
{
	internal class UtilityInjectionModule : NinjectModule

	{
		public override void Load()
		{
			Bind<IFileSystem>().To<FileSystem>();
			Bind<ITypeActivator>().To<TypeActivator>();
		}
	}
}