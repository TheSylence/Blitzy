using System.Diagnostics.CodeAnalysis;
using Blitzy.ViewModels;
using System.Windows;
using System.Windows.Interactivity;

namespace Blitzy.Behaviors
{
	[ExcludeFromCodeCoverage]
	internal class CloseWindow : Behavior<Window>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			var controller = AssociatedObject.DataContext as IWindowController;
			if( controller != null )
			{
				controller.CloseRequested += Controller_CloseRequested;
			}
		}

		private void Controller_CloseRequested( object sender, CloseEventArgs e )
		{
			if( e.Result != null )
			{
				AssociatedObject.DialogResult = e.Result;
			}

			AssociatedObject.Close();
		}
	}
}