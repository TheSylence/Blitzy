using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blitzy.PluginInterfaces.Commands
{
	/// <summary>
	/// Node in the command tree that is used by the user to navigate
	/// commands
	/// </summary>
	public interface ICommandNode
	{
		/// <summary>
		/// Executes this command asynchronously
		/// </summary>
		/// <param name="data">The data the user entered</param>
		/// <param name="primary">Execute in primary or secondary mode</param>
		/// <returns>Result of the execution</returns>
		Task<CommandResult> Execute( string data, bool primary );

		/// <summary>
		/// Gets a list of all available child nodes
		/// </summary>
		/// <param name="data">User entered data that is used to filter</param>
		/// <returns></returns>
		IEnumerable<ICommandNode> GetChildNodes( string data );

		/// <summary>
		/// Description of this node
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Data of the node's icon (or null if no icon)
		/// </summary>
		byte[] IconData { get; }

		/// <summary>
		/// Name of this node
		/// </summary>
		string Name { get; }
	}
}