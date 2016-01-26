using System;

namespace Blitzy.ViewModels
{
	interface IWindowController
	{
		event EventHandler<CloseEventArgs> CloseRequested;
	}

	internal class CloseEventArgs : EventArgs
	{
		public CloseEventArgs( bool? result = null )
		{
			Result = result;
		}

		public bool? Result { get; }

		public static CloseEventArgs Cancel = new CloseEventArgs( false );
		public static CloseEventArgs Ok = new CloseEventArgs( true );
	}
}