using Blitzy.Models;
using Blitzy.Models.Plugins;
using Blitzy.PluginInterfaces;
using Blitzy.Utilities;
using Blitzy.ViewModels;
using Blitzy.ViewModels.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blitzy.Tests.ViewModels.Settings
{
	[TestClass]
	public class SettingsDialogViewModelTests
	{
		[TestMethod, TestCategory( "ViewModels.Settings" )]
		public void NotifyPropertyChangedIsImplementedCorrectly()
		{
			// Arrange
			var settings = new Mock<ISettings>();
			var pluginContainer = new Mock<IPluginContainer>();

			var vm = new SettingsDialogViewModel( settings.Object, pluginContainer.Object );
			var tester = new PropertyChangedTester( vm );

			tester.RegisterTypeFactory( typeof( ITreeViewItemViewModel ), () => new Mock<ITreeViewItemViewModel>().Object );

			// Act
			tester.Test( nameof( SettingsDialogViewModel.AppThemes ) );

			// Assert
			tester.Verify();
		}

		[TestMethod, TestCategory( "ViewModels.Settings" )]
		public async Task SettingsTreeIsCorrecltyPopulated()
		{
			// Arrange
			var settings = new Mock<ISettings>();
			var pluginContainer = new Mock<IPluginContainer>();
			pluginContainer.SetupGet( p => p.LoadedPlugins ).Returns( new List<IPlugin>() );
			var appThemes = new Mock<IAppThemes>();

			var vm = new SettingsDialogViewModel( settings.Object, pluginContainer.Object )
			{
				AppThemes = appThemes.Object
			};

			// Act
			await vm.OnLoad( null );

			// Assert
			Assert.IsTrue( vm.TopLevelItems.Any() );
		}
	}
}