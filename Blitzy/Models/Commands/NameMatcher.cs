using System;
using System.Globalization;
using System.Linq;

namespace Blitzy.Models.Commands
{
	internal class NameMatcher
	{
		public bool Matches( string commandName, string input )
		{
			if( string.IsNullOrWhiteSpace( commandName ) )
			{
				return false;
			}
			if( string.IsNullOrWhiteSpace( input ) )
			{
				return false;
			}

			string name = commandName.ToLowerInvariant();
			var words = name.Split( SplitChars, StringSplitOptions.RemoveEmptyEntries );

			if( name.Contains( input ) )
			{
				return true;
			}

			bool match = true;

			// Find: Metallica - Nothing else matters
			// Input Metallica nothing
			string[] inputWords = input.Split( SplitChars, StringSplitOptions.RemoveEmptyEntries );
			for( int i = 0; i < words.Length && i < inputWords.Length; ++i )
			{
				if( words[i].Contains( inputWords[i] ) )
				{
					continue;
				}

				match = false;
				break;
			}

			if( !match )
			{
				match = words.Intersect( inputWords ).Count() == inputWords.Length;
			}

			// Find: Visual Studio Command Prompt
			// Input: vscp
			if( match )
			{
				return true;
			}

			if( input.Length > words.Length )
			{
				return false;
			}

			match = true;
			for( int i = 0; i < input.Length && i <= words.Length; ++i )
			{
				if( words[i].StartsWith( input[i].ToString( CultureInfo.CurrentUICulture ), true, CultureInfo.CurrentUICulture ) )
				{
					continue;
				}

				match = false;
				break;
			}

			return match;
		}

		private static readonly char[] SplitChars = { ' ', '-', '.', ',', ';', '/', '\\' };
	}
}