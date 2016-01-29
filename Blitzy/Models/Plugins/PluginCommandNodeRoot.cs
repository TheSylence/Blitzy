using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blitzy.PluginInterfaces;
using Blitzy.PluginInterfaces.Commands;

namespace Blitzy.Models.Plugins
{
	class PluginCommandNodeRoot : CommandNode
	{
		private readonly ICommandNode[] Children;

		public PluginCommandNodeRoot( IPlugin plugin )
		{
			Name = plugin.Name;
			Description = plugin.Description;
			AcceptsData = false;

			Children = plugin.GetNodes().ToArray();
		}

		public override IEnumerable<ICommandNode> GetChildNodes()
		{
			return Children;
		}

		public override Task<CommandResult> Execute( string data, bool primary )
		{
			return Task.FromResult( CommandResult.Success );
		}
		
		public override string Description { get; }
		
		public override string Name { get; }
	}
}