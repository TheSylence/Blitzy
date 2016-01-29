using System.Linq;
using Blitzy.Models;
using Blitzy.Models.Commands;
using Blitzy.PluginInterfaces.Commands;
using Blitzy.ViewModels.Main;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Blitzy.Tests.ViewModels.Main
{
	[TestClass]
	public class InputProcessorTests
	{
		[TestMethod, TestCategory( "Models" )]
		public void AutoCompleteKeepsUserData()
		{
			// Arrange
			var settingsMock = new Mock<ISettings>();

			var cmdMock = new Mock<ICommandNode>();
			cmdMock.SetupGet( c => c.AcceptsData ).Returns( true );
			cmdMock.SetupGet( c => c.Name ).Returns( "test" );

			var tree = new Mock<ICommandTree>();
			IInputProcessor processor = new InputProcessor( tree.Object, settingsMock.Object );

			// Act
			string text = processor.AutoCompleteInput( "test" + processor.Separator + "abc", cmdMock.Object );

			// Assert
			Assert.AreEqual( "test" + processor.Separator + "abc", text );
		}

		[TestMethod, TestCategory( "Models" )]
		public void AutoCompleteSingleCommandIsWorking()
		{
			// Arrange
			var settingsMock = new Mock<ISettings>();
			var repo = new Mock<ICommandTree>();

			var cmdMock = new Mock<ICommandNode>();
			cmdMock.SetupGet( c => c.Name ).Returns( "test" );
			cmdMock.SetupGet( c => c.AcceptsData ).Returns( false );

			IInputProcessor processor = new InputProcessor( repo.Object, settingsMock.Object );

			// Act
			string autocompleted = processor.AutoCompleteInput( "te", cmdMock.Object );

			// Assert
			Assert.AreEqual( "test", autocompleted );
		}

		[TestMethod, TestCategory( "Models" )]
		public void AutoCompleteStopsAtUserData()
		{
			// Arrange
			var settingsMock = new Mock<ISettings>();

			var parentCmdMock = new Mock<ICommandNode>();
			parentCmdMock.SetupGet( c => c.AcceptsData ).Returns( true );
			parentCmdMock.SetupGet( c => c.Name ).Returns( "parent" );

			var cmdMock = new Mock<ICommandNode>();
			cmdMock.SetupGet( c => c.AcceptsData ).Returns( true );
			cmdMock.SetupGet( c => c.Name ).Returns( "test" );
			cmdMock.SetupGet( c => c.Parent ).Returns( parentCmdMock.Object );

			var repo = new Mock<ICommandTree>();

			IInputProcessor processor = new InputProcessor( repo.Object, settingsMock.Object );

			// Act
			string text = processor.AutoCompleteInput( "parent" + processor.Separator + "test",
				cmdMock.Object );

			// Assert
			Assert.AreEqual( "parent" + processor.Separator + "test" + processor.Separator, text );
		}

		[TestMethod, TestCategory( "Models" )]
		public void AutoCompleteSubCommandIsWorking()
		{
			// Arrange
			var settingsMock = new Mock<ISettings>();
			var repo = new Mock<ICommandTree>();

			var subCmdMock = new Mock<ICommandNode>();
			subCmdMock.SetupGet( c => c.Name ).Returns( "sub" );
			subCmdMock.SetupGet( c => c.AcceptsData ).Returns( false );

			var cmdMock = new Mock<ICommandNode>();
			cmdMock.SetupGet( c => c.Name ).Returns( "test" );
			cmdMock.SetupGet( c => c.AcceptsData ).Returns( false );

			subCmdMock.SetupGet( c => c.Parent ).Returns( cmdMock.Object );

			IInputProcessor processor = new InputProcessor( repo.Object, settingsMock.Object );

			// Act
			string autocompleted = processor.AutoCompleteInput( "test" + processor.Separator + "s", subCmdMock.Object );

			// Assert
			Assert.AreEqual( "test" + processor.Separator + "sub", autocompleted );
		}

		[TestMethod, TestCategory( "Models" )]
		public void CommandDataInMultiCommandIsExtractedCorrectly()
		{
			// Arrange
			var settingsMock = new Mock<ISettings>();
			var repo = new Mock<ICommandTree>();

			IInputProcessor processor = new InputProcessor( repo.Object, settingsMock.Object );

			// Act
			var data = processor.ExtractCommandData( "name" + processor.Separator + "data" );

			// Assert
			Assert.AreEqual( "data", data );
		}

		[TestMethod, TestCategory( "Models" )]
		public void CommandDataInSingleCommandIsExtractedCorrectly()
		{
			// Arrange
			var settingsMock = new Mock<ISettings>();
			var repo = new Mock<ICommandTree>();

			IInputProcessor processor = new InputProcessor( repo.Object, settingsMock.Object );

			// Act
			var data = processor.ExtractCommandData( "test" );

			// Assert
			Assert.IsNotNull( data );
		}

		[TestMethod, TestCategory( "Models" )]
		public void CommandMatchesAreCorrect()
		{
			// Arrange
			var settingsMock = new Mock<ISettings>();
			settingsMock.SetupGet( s => s.MaxMatchingItems ).Returns( int.MaxValue );

			var cmdMock = new Mock<ICommandNode>();
			cmdMock.SetupGet( c => c.Name ).Returns( "test" );

			var repo = new Mock<ICommandTree>();
			repo.Setup( r => r.GetRootNodes( It.IsAny<string>() ) ).Returns( new[] {cmdMock.Object} );
			IInputProcessor processor = new InputProcessor( repo.Object, settingsMock.Object );

			// Act
			var commands = processor.MatchedCommands( "test" ).ToArray();

			// Assert
			Assert.AreEqual( 1, commands.Count() );
			Assert.AreSame( cmdMock.Object, commands.First() );
			
			settingsMock.Verify( s => s.MaxMatchingItems, Times.Once() );
		}

		[TestMethod, TestCategory( "Models" )]
		public void SubCommandMatchesAreCorrect()
		{
			// Arrange
			var settingsMock = new Mock<ISettings>();
			settingsMock.SetupGet( s => s.MaxMatchingItems ).Returns( int.MaxValue );

			var subCmdMock = new Mock<ICommandNode>();
			subCmdMock.SetupGet( c => c.Name ).Returns( "test" );
			//subCmdMock.SetupGet( c => c.ExecutionCount ).Returns( 1 );
			subCmdMock.SetupGet( c => c.AcceptsData ).Returns( false );

			var subCmd = subCmdMock.Object;

			var cmdMock = new Mock<ICommandNode>();
			//cmdMock.SetupGet( c => c.SubCommands ).Returns( new[] { subCmd } );
			cmdMock.SetupGet( c => c.AcceptsData ).Returns( true );
			//cmdMock.SetupGet( c => c.ExecutionCount ).Returns( 1 );
			cmdMock.SetupGet( c => c.Name ).Returns( "test" );

			var repo = new Mock<ICommandTree>();
			repo.Setup( r => r.GetChildNodes( It.IsAny<string>(), cmdMock.Object ) ).Returns( new[] { subCmd } );
			IInputProcessor processor = new InputProcessor( repo.Object, settingsMock.Object );

			// Act
			var commands = processor.MatchedCommands( "test" + processor.Separator + "sub", cmdMock.Object ).ToArray();

			// Assert
			Assert.AreEqual( 1, commands.Count() );
			Assert.AreSame( subCmd, commands.First() );

			//cmdMock.Verify( c => c.SubCommands, Times.AtLeastOnce() );
			settingsMock.Verify( s => s.MaxMatchingItems, Times.Once() );
		}

		[TestMethod, TestCategory( "Models" )]
		public void SubCommandMatchesWithEmptyLastArgAreCorrect()
		{
			// Arrange
			var settingsMock = new Mock<ISettings>();
			settingsMock.SetupGet( s => s.MaxMatchingItems ).Returns( int.MaxValue );

			var subCmdMock = new Mock<ICommandNode>();
			subCmdMock.SetupGet( c => c.Name ).Returns( "sub" );

			var subCmd = subCmdMock.Object;

			var cmdMock = new Mock<ICommandNode>();
			cmdMock.Setup( c => c.GetChildNodes() ).Returns( new[] {subCmd} );
			cmdMock.SetupGet( c => c.Name ).Returns( "test" );

			var repo = new Mock<ICommandTree>();
			repo.Setup( r => r.GetChildNodes( It.IsAny<string>(), cmdMock.Object ) ).Returns( new[] {subCmd} );
			IInputProcessor processor = new InputProcessor( repo.Object, settingsMock.Object );

			// Act
			var commands = processor.MatchedCommands( "test" + processor.Separator, cmdMock.Object ).ToArray();

			// Assert
			Assert.AreEqual( 1, commands.Count() );
			Assert.AreSame( subCmd, commands.First() );

			//cmdMock.Verify( c => c.SubCommands, Times.AtLeastOnce() );
			settingsMock.Verify( s => s.MaxMatchingItems, Times.Once() );
		}
	}
}