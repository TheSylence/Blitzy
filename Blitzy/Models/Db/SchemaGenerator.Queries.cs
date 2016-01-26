using System;
using System.Collections.Generic;
using System.Linq;

namespace Blitzy.Models.Db
{
	partial class SchemaGenerator
	{
		private static IEnumerable<string> S00()
		{
			yield return "CREATE TABLE data ( key TEXT PRIMARY KEY NOT NULL, value TEXT NOT NULL, expires INTEGER NOT NULL );";

			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.Accent" }, { "value", "Blue" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.CheckForUpdates" }, { "value", "True" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.PreviewUpdates" }, { "value", "False" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.CloseAfterExecution" }, { "value", "True" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.CloseOnEscape" }, { "value", "True" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.CloseOnFocusLost" }, { "value", "True" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.HotKey" }, { "value", "Space" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.HotKeyModifier" }, { "value", "Alt" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.KeepInputContent" }, { "value", "False" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.MaxMatchingItems" }, { "value", "20" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.ScrollThroughCommandList" }, { "value", "True" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.ShowTrayIcon" }, { "value", "True" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
			yield return InsertInto( "data", new Dictionary<string, string> { { "key", "Blitzy.Settings.Theme" }, { "value", "BaseLight" }, { "expires", DateTime.MaxValue.Ticks.ToString() } } );
		}

		private IEnumerable<string[]> GenerateQueries()
		{
			yield return S00().ToArray();
		}
	}
}