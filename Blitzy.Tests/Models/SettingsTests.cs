using Blitzy.Models;
using Blitzy.PluginInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Blitzy.Tests.Models
{
	[TestClass]
	public class SettingsTests
	{
		[TestMethod, TestCategory( "Models" )]
		public async Task SettingsAreCorrectlyLoaded()
		{
			// Arrange
			var db = new Mock<IDatabase>( MockBehavior.Strict );

			db.Setup( x => x.Get<string>( "Blitzy.Settings.Accent" ) ).Returns( Task.FromResult( "the accent" ) );
			db.Setup( x => x.Get<bool>( "Blitzy.Settings.CheckForUpdates" ) ).Returns( Task.FromResult( true ) );
			db.Setup( x => x.Get<bool>( "Blitzy.Settings.PreviewUpdates" ) ).Returns( Task.FromResult( true ) );
			db.Setup( x => x.Get<bool>( "Blitzy.Settings.CloseAfterExecution" ) ).Returns( Task.FromResult( true ) );
			db.Setup( x => x.Get<bool>( "Blitzy.Settings.CloseOnEscape" ) ).Returns( Task.FromResult( true ) );
			db.Setup( x => x.Get<bool>( "Blitzy.Settings.CloseOnFocusLost" ) ).Returns( Task.FromResult( true ) );
			db.Setup( x => x.Get<string>( "Blitzy.Settings.HotKey" ) ).Returns( Task.FromResult( "Hot" ) );
			db.Setup( x => x.Get<string>( "Blitzy.Settings.HotKeyModifier" ) ).Returns( Task.FromResult( "Mod" ) );
			db.Setup( x => x.Get<bool>( "Blitzy.Settings.KeepInputContent" ) ).Returns( Task.FromResult( true ) );
			db.Setup( x => x.Get<int>( "Blitzy.Settings.MaxMatchingItems" ) ).Returns( Task.FromResult( 123 ) );
			db.Setup( x => x.Get<bool>( "Blitzy.Settings.ScrollThroughCommandList" ) ).Returns( Task.FromResult( true ) );
			db.Setup( x => x.Get<bool>( "Blitzy.Settings.ShowTrayIcon" ) ).Returns( Task.FromResult( true ) );
			db.Setup( x => x.Get<string>( "Blitzy.Settings.Theme" ) ).Returns( Task.FromResult( "the theme" ) );

			var settings = new Settings( db.Object );

			// Act
			await settings.Load();
			
			// Assert
			db.VerifyAll();

			Assert.AreEqual( "the accent", settings.Accent );
			Assert.AreEqual( true, settings.CheckForUpdates );
			Assert.AreEqual( true, settings.PreviewUpdates );
			Assert.AreEqual( true, settings.CloseAfterExecution );
			Assert.AreEqual( true, settings.CloseOnEscape );
			Assert.AreEqual( true, settings.CloseOnFocusLost );
			Assert.AreEqual( "Hot", settings.HotKey );
			Assert.AreEqual( "Mod", settings.HotKeyModifier );
			Assert.AreEqual( true, settings.KeepInputContent );
			Assert.AreEqual( 123, settings.MaxMatchingItems );
			Assert.AreEqual( true, settings.ScrollThroughCommandList );
			Assert.AreEqual( true, settings.ShowTrayIcon );
			Assert.AreEqual( "the theme", settings.Theme );
		}

		[TestMethod, TestCategory( "Models" )]
		public async Task SettingsAreCorrectlySaved()
		{
			// Arrange
			var db = new Mock<IDatabase>( MockBehavior.Strict );

			db.Setup( x => x.Set( "Blitzy.Settings.Accent", "the accent", null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.CheckForUpdates", false, null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.PreviewUpdates", true, null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.CloseAfterExecution", false, null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.CloseOnEscape", true, null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.CloseOnFocusLost", false, null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.HotKey", "Hotkey", null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.HotKeyModifier", "Mod", null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.KeepInputContent", true, null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.MaxMatchingItems", 123, null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.ScrollThroughCommandList", false, null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.ShowTrayIcon", true, null ) ).Returns( Task.CompletedTask ).Verifiable();
			db.Setup( x => x.Set( "Blitzy.Settings.Theme", "the theme", null ) ).Returns( Task.CompletedTask ).Verifiable();

			var settings = new Settings( db.Object )
			{
				Accent = "the accent",
				CheckForUpdates = false,
				PreviewUpdates = true,
				CloseAfterExecution = false,
				CloseOnEscape = true,
				CloseOnFocusLost = false,
				HotKey = "Hotkey",
				HotKeyModifier = "Mod",
				KeepInputContent = true,
				MaxMatchingItems = 123,
				ScrollThroughCommandList = false,
				ShowTrayIcon = true,
				Theme = "the theme"
			};

			// Act
			await settings.Save();

			// Assert
			db.VerifyAll();
		}
	}
}