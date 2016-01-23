using System.Collections.Generic;
using Blitzy.PluginInterfaces.Commands;

namespace Blitzy.PluginInterfaces
{
	/// <summary>
	/// Implement this if your plugin wants to inject <see cref="ICommandNode"/> into the command tree.
	/// </summary>
	public interface IProvideCommandNodes
	{
		/// <summary>
		/// Called to retrieve all top command nodes your plugin offers.
		/// </summary>
		/// <returns></returns>
		IEnumerable<ICommandNode> GetNodes();
	}
}