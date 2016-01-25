using Blitzy.Models;
using Blitzy.Models.Commands;
using Blitzy.Models.Db;
using Blitzy.Models.Plugins;
using Blitzy.PluginInterfaces;
using Blitzy.Utilities;
using Blitzy.ViewModels.Main;
using Blitzy.ViewModels.Settings;
using Ninject;
using System.Windows;

namespace Blitzy
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override void OnStartup( StartupEventArgs e )
		{
			Kernel = SetupKernel();

			base.OnStartup( e );
		}

		private static IKernel SetupKernel()
		{
			var kernel = new StandardKernel();

			kernel.Bind<IDatabase>().ToProvider<DatabaseProvider>().InSingletonScope();
			kernel.Bind<IPluginContainer>().To<PluginContainer>().InSingletonScope();
			kernel.Bind<IPluginHost>().To<PluginHost>().InSingletonScope();
			kernel.Bind<ISettings>().To<Settings>();
			kernel.Bind<ICommandTree>().To<CommandTree>();

			kernel.Bind<IFileSystem>().To<FileSystem>();
			kernel.Bind<ITypeActivator>().To<TypeActivator>();

			kernel.Bind<IMainViewModel>().To<MainViewModel>();
			kernel.Bind<ISettingsDialogViewModel>().To<SettingsDialogViewModel>();
			kernel.Bind<ICommandController>().To<CommandController>();
			kernel.Bind<IInputProcessor>().To<InputProcessor>();

			return kernel;
		}

		public static IKernel Kernel { get; private set; }
	}
}