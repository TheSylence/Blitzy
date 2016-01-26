using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Blitzy.Resources;

namespace Blitzy.ViewModels.Settings
{
	internal class AboutViewModel : TreeViewItemViewModel
	{
		public AboutViewModel() : base( null, Strings.About )
		{
			var asm = Assembly.GetExecutingAssembly();
			var name = asm.GetName();

			Version = name.Version;
			BuildDate = ExtractBuildDate( asm );

			var fvi = FileVersionInfo.GetVersionInfo( asm.Location );
			var match = ShaPattern.Match( fvi.ProductVersion );

			GitVersion = match.Groups[1].Value.Substring( 0, 8 );
		}

		private static DateTime ExtractBuildDate( Assembly asm )
		{
			string filePath = asm.Location;

			if( DateTime.Now.Year < 2038 )
			{
				const int peHeaderOffset = 60;
				const int linkerTimestampOffset = 8;
				byte[] b = new byte[2048];
				Stream s = null;
				try
				{
					s = new FileStream( filePath, FileMode.Open, FileAccess.Read );
					s.Read( b, 0, 2048 );
				}
				finally
				{
					s?.Close();
				}
				int i = BitConverter.ToInt32( b, peHeaderOffset );

				int secondsSince1970 = BitConverter.ToInt32( b, i + linkerTimestampOffset );
				DateTime dt = new DateTime( 1970, 1, 1, 0, 0, 0 );
				dt = dt.AddSeconds( secondsSince1970 );

				int utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset( dt ).Hours;

				dt = dt.AddHours( utcOffset );
				return dt;
			}
			return File.GetLastWriteTime( filePath );
		}

		public DateTime BuildDate { get; }
		public string GitVersion { get; }
		public Version Version { get; }
		private static readonly Regex ShaPattern = new Regex( "Sha:(\\w+)", RegexOptions.Compiled );
	}
}