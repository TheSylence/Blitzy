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
		///     Determines whether this command can be executed with the specified data.
		/// </summary>
		/// <param name="primary">Flag indicating whether primary (true) or secondary (false) execution mode is used.</param>
		/// <param name="commandData">User entered data for this command.</param>
		/// <returns><c>true</c> if the command can be executed; otherwise <c>false</c>.</returns>
		bool CanExecute( bool primary, string commandData = null );

		/// <summary>
		/// Executes this command asynchronously
		/// </summary>
		/// <param name="data">User entered data for this command.</param>
		/// <param name="primary">Flag indicating whether primary (true) or secondary (false) execution mode is used.</param>
		/// <returns>Result of the execution</returns>
		Task<CommandResult> Execute( string data, bool primary );

		/// <summary>
		/// Gets a list of all available child nodes
		/// </summary>
		/// <returns>List of all availabe child nodes.</returns>
		IEnumerable<ICommandNode> GetChildNodes();

		/// <summary>
		///     Gets or sets a value indicating whether this commands accept user inputs.
		/// </summary>
		/// <value>
		///     <c>true</c> if this command accepts user input; <c>false</c> if the command only holds other commands.
		/// </value>
		bool AcceptsData { get; }

		/// <summary>
		/// Description of this node
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Data of the node's icon (or null if no icon)
		/// </summary>
		byte[] IconData { get; }

		/// <summary>
		/// Gets the parent of this node.
		/// </summary>
		ICommandNode Parent { get; }

		/// <summary>
		/// Name of this node
		/// </summary>
		string Name { get; }
	}
}