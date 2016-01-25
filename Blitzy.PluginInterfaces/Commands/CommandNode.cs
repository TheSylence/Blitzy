using System.Collections.Generic;
using System.Linq;
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
		///     Determines whether this command can be executed with the specified data.
		/// </summary>
		/// <param name="primary">Flag indicating whether primary (true) or secondary (false) execution mode is used.</param>
		/// <param name="commandData">User entered data for this command.</param>
		/// <returns><c>true</c> if the command can be executed; otherwise <c>false</c>.</returns>
		public virtual bool CanExecute( bool primary, string commandData = null )
		{
			return true;
		}

		/// <summary>
		/// Executes this command asynchronously
		/// </summary>
		/// <param name="data">User entered data for this command.</param>
		/// <param name="primary">Flag indicating whether primary (true) or secondary (false) execution mode is used.</param>
		/// <returns>Result of the execution</returns>
		public abstract Task<CommandResult> Execute( string data, bool primary );

		/// <summary>
		/// Gets a list of all available child nodes
		/// </summary>
		/// <returns>List of all availabe child nodes.</returns>
		public IEnumerable<ICommandNode> GetChildNodes()
		{
			return Enumerable.Empty<ICommandNode>();
		}

		/// <summary>
		///     Gets or sets a value indicating whether this commands accept user inputs.
		/// </summary>
		/// <value>
		///     <c>true</c> if this command accepts user input; <c>false</c> if the command only holds other commands.
		/// </value>
		public bool AcceptsData { get; } = false;

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

		/// <summary>
		/// Gets the parent of this node.
		/// </summary>
		public ICommandNode Parent { get; }
	}
}