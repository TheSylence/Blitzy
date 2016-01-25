using System.Windows.Media;
using Blitzy.PluginInterfaces.Commands;

namespace Blitzy.ViewModels.Main
{
	internal interface ICommandViewModel
	{
		ICommandNode Command { get; }
		string Description { get; }
		ImageSource Icon { get; }
		string Name { get; }
	}

	internal class CommandViewModel : ICommandViewModel
	{
		public CommandViewModel( ICommandNode node )
		{
			Command = node;
		}

		public ICommandNode Command { get; }
		public string Description => Command.Description;
		public ImageSource Icon { get; }
		public string Name => Command.Name;
	}
}