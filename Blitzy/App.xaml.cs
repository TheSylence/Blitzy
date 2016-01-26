using System;
using System.Reflection;
using System.Text;
using System.Windows;
using Anotar.NLog;
using Blitzy.Models;
using Blitzy.Models.Commands;
using Blitzy.Models.Db;
using Blitzy.Models.Plugins;
using Blitzy.PluginInterfaces;
using Blitzy.Services;
using Blitzy.Utilities;
using Blitzy.ViewModels.Main;
using Blitzy.ViewModels.Settings;
using Ninject;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Blitzy
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override void OnStartup( StartupEventArgs e )
		{
			SetupLogging();
			LogTo.Info( "Application start " );
			LogEnvironmentInfo();

			Kernel = SetupKernel();

			base.OnStartup( e );
		}

		static void LogEnvironmentInfo()
		{
			LogTo.Info( "Version {0}", Assembly.GetExecutingAssembly().GetName().Version );
			LogTo.Info( "CLR: {0}", Environment.Version );
			LogTo.Info( "{0} ({1})", Environment.OSVersion, Environment.Is64BitOperatingSystem ? "x64" : "x86" );
			LogTo.Info( "{0}bit process", Environment.Is64BitProcess ? 64 : 32 );
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

			kernel.Bind<IServiceRepository>().To<ServiceRepository>();
			kernel.Bind<ISettingsDialogService>().To<SettingsDialogService>();

			return kernel;
		}

		void SetupLogging()
		{
			var config = LogManager.Configuration ?? new LoggingConfiguration();
			if( config.LoggingRules.Count > 0 )
			{
				return;
			}

			var debuggerTarget = new DebuggerTarget
			{
				Layout = "[${whenEmpty:whenEmpty=${threadId}:inner=${threadname}}] ${logger} ${message}"
			};

			config.AddTarget( "debugger", debuggerTarget );

			var fileTarget = new FileTarget
			{
				Encoding = Encoding.UTF8,
				MaxArchiveFiles = 10,
				ArchiveNumbering = ArchiveNumberingMode.Date,
				ArchiveEvery = FileArchivePeriod.Day,
				ArchiveDateFormat = "yyyy-MM-dd",
				FileName = "${specialfolder:folder=ApplicationData}/btbsoft/Blitzy/log.txt",
				ArchiveFileName = "${specialfolder:folder=ApplicationData}/btbsoft/Blitzy/log.{#}.txt",
				Layout = "${date} ${pad:padding=5:inner=${level:uppercase=true}} ${logger} - ${message}"
			};

			config.AddTarget( "file", fileTarget );

			var debuggerRule = new LoggingRule( "*", LogLevel.Trace, debuggerTarget );
			config.LoggingRules.Add( debuggerRule );

			var fileRule = new LoggingRule( "*", LogLevel.Trace, fileTarget );
			config.LoggingRules.Add( fileRule );

			LogManager.Configuration = config;
		}

		public static IKernel Kernel { get; private set; }
	}
}