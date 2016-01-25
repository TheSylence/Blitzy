using System.Collections.Generic;
using System.Linq;
using Blitzy.PluginInterfaces.Commands;

namespace Blitzy.Models.Commands
{
	internal interface ICommandTree
	{
		IEnumerable<ICommandNode> GetChildNodes( string input, ICommandNode node );

		IEnumerable<ICommandNode> GetRootNodes( string input );

		void InjectRoot( ICommandNode node );
	}

	internal class CommandTree : ICommandTree
	{
		public CommandTree()
		{
			NameMatch = new NameMatcher();
			RootNodes = new List<ICommandNode>();
		}

		public IEnumerable<ICommandNode> GetChildNodes( string input, ICommandNode node )
		{
			var children = node.GetChildNodes().ToArray();

			if( !children.Any() )
			{
				return new[] { node };
			}

			if( !string.IsNullOrEmpty( input ) )
			{
				children = children.Where( cmd => NameMatch.Matches( cmd.Name, input ) ).ToArray();
			}

			return children;
		}

		public IEnumerable<ICommandNode> GetRootNodes( string input )
		{
			return RootNodes.Where( cmd => NameMatch.Matches( cmd.Name, input ) );
		}

		public void InjectRoot( ICommandNode node )
		{
			RootNodes.Add( node );
		}

		private readonly NameMatcher NameMatch;

		private readonly List<ICommandNode> RootNodes;
	}
}