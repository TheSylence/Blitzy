using System;
using System.Collections.Generic;
using System.Linq;
using Blitzy.Models;
using Blitzy.Models.Commands;
using Blitzy.PluginInterfaces.Commands;

namespace Blitzy.ViewModels.Main
{
	public interface IInputProcessor
	{
		string AutoCompleteInput( string input, ICommandNode currentCommand );

		string ExtractCommandData( string textInput );

		IEnumerable<ICommandNode> MatchedCommands( string input, ICommandNode currentCommand = null );

		string Separator { get; }
	}

	internal class InputProcessor : IInputProcessor
	{
		public InputProcessor( ICommandTree tree, ISettings settings )
		{
			CommandTree = tree;
			Settings = settings;
			Separator = " " + char.ConvertFromUtf32( 0x00002192 ) + " ";
		}

		public string AutoCompleteInput( string input, ICommandNode currentCommand )
		{
			List<string> parts = new List<string>( GetCommandParts( input ) );

			ICommandNode item = currentCommand;
			var itemChain = new List<ICommandNode>();
			for( int i = parts.Count - 1; i >= 0; --i )
			{
				if( item == null )
				{
					break;
				}

				itemChain.Add( item );
				item = item.Parent;
			}
			itemChain.Reverse();

			if( parts.Count > itemChain.Count )
			{
				return input;
			}

			// Autocomplete all commands until a command that accepts input data is reached
			for( int i = 0; i < parts.Count; ++i )
			{
				parts[i] = itemChain[i].Name;

				if( itemChain[i].AcceptsData )
				{
					break;
				}
			}

			if( currentCommand.AcceptsData )
			{
				parts.Add( string.Empty );
			}

			return string.Join( Separator, parts );
		}

		public string ExtractCommandData( string textInput )
		{
			var parts = GetCommandParts( textInput );

			return parts.LastOrDefault();
		}

		public IEnumerable<ICommandNode> MatchedCommands( string input, ICommandNode currentCommand = null )
		{
			string[] parts = GetCommandParts( input );
			string command = parts[0].ToLowerInvariant();

			var matchedCommands = new List<ICommandNode>();
			if( parts.Length == 1 )
			{
				matchedCommands.AddRange( CommandTree.GetRootNodes( input ) );
			}
			else if( currentCommand != null )
			{
				matchedCommands.AddRange( CommandTree.GetChildNodes( input, currentCommand ) );
			}

			return matchedCommands
				.OrderByDescending( cmd => DiceCoefficent( cmd.Name, command ) )
				.Take( Settings.MaxMatchingItems )
				.ToArray();
		}

		private static double DiceCoefficent( string str, string other, int n = 3 )
		{
			string[] strGrams = GetNGrams( str, n );
			string[] otherGrams = GetNGrams( other, n );

			int matches = strGrams.Intersect( otherGrams ).Count();

			return ( 2.0 * matches ) / ( strGrams.Length + otherGrams.Length );
		}

		private static string[] GetNGrams( string str, int n )
		{
			string[] grams = new string[(int)Math.Ceiling( str.Length / (double)n )];
			int idx = 0;
			for( int i = 0; i < grams.Length; ++i )
			{
				if( idx + n > str.Length )
				{
					grams[i] = str.Substring( idx );
				}
				else
				{
					grams[i] = str.Substring( idx, n );
				}

				idx += n;
			}

			return grams;
		}

		private string[] GetCommandParts( string input )
		{
			IEnumerable<string> parts = input.Split( new[] { Separator }, StringSplitOptions.None );

			if( input.Trim().EndsWith( Separator.Trim(), StringComparison.Ordinal ) )
			{
				parts = parts.Concat( new[] { string.Empty } );
			}

			return parts.ToArray();
		}

		public string Separator { get; }
		private readonly ICommandTree CommandTree;
		private readonly ISettings Settings;
	}
}