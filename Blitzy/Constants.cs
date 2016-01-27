using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Blitzy
{
	[ExcludeFromCodeCoverage]
	internal static class Constants
	{
		internal static string DatabaseFile => Path.Combine( AppFolder, "data.db3" );

		private static string AppFolder
		{
			get
			{
				string folder = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "btbsoft",
					"Blitzy" );

				if( !Directory.Exists( folder ) )
				{
					Directory.CreateDirectory( folder );
				}

				return folder;
			}
		}
	}
}