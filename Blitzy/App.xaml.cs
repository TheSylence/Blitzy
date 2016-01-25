using Blitzy.Injections;
using Ninject;
using Ninject.Modules;
using System.Collections.Generic;
using System.Linq;
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
			Kernel = new StandardKernel( InjectionModules.ToArray() );

			base.OnStartup( e );
		}

		public static IKernel Kernel { get; private set; }

		private static IEnumerable<INinjectModule> InjectionModules
		{
			get
			{
				yield return new UtilityInjectionModule();
				yield return new ModelInjectionModule();
				yield return new ViewModelInjectionModule();
			}
		}
	}
}