using Blitzy.Models;
using Blitzy.Models.Commands;
using Blitzy.Models.Plugins;
using Blitzy.PluginInterfaces;
using Blitzy.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Blitzy.Tests.Models.Plugins
{
	[TestClass]
	public class PluginContainerTests
	{
		[TestMethod, TestCategory( "Models.Plugins" )]
		public async Task PluginsAreLoadedFromTheCorrectPath()
		{
			// Arrange
			var host = new Mock<IPluginHost>();
			var settings = new Mock<ISettings>();
			var tree = new Mock<ICommandTree>();

			var fileSystem = new Mock<IFileSystem>();

			var container = new PluginContainer( host.Object, tree.Object, settings.Object )
			{
				FileSystem = fileSystem.Object
			};

			// Act
			await container.LoadPlugins();

			// Assert
			fileSystem.Verify( fs => fs.ListFiles( "plugins", "*.dll", System.IO.SearchOption.AllDirectories ), Times.Once() );
		}
	}
}