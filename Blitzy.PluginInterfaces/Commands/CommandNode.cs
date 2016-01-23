using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blitzy.PluginInterfaces.Commands
{
	/// <summary>
	/// Default implementation of the <see cref="ICommandNode"/> interface.
	/// </summary>
	/// <remarks>Use this as a starting point when implementing commands.</remarks>
	public abstract class CommandNode : ICommandNode
	{
		/// <summary>
		/// Executes this command asynchronously
		/// </summary>
		/// <param name="data">The data the user entered</param>
		/// <param name="primary">Execute in primary or secondary mode</param>
		/// <returns>Result of the execution</returns>
		public abstract Task<CommandResult> Execute( string data, bool primary );

		/// <summary>
		/// Gets a list of all available child nodes
		/// </summary>
		/// <param name="data">User entered data that is used to filter</param>
		/// <returns></returns>
		public virtual IEnumerable<ICommandNode> GetChildNodes( string data )
		{
			yield break;
		}

		/// <summary>
		/// Description of this node
		/// </summary>
		public abstract string Description { get; }

		/// <summary>
		/// Data of the node's icon (or null if no icon)
		/// </summary>
		public virtual byte[] IconData { get; } = null;

		/// <summary>
		/// Name of this node
		/// </summary>
		public abstract string Name { get; }
	}
}