using System;

namespace Blitzy.ViewModels
{
	interface IWindowController
	{
		event EventHandler<CloseEventArgs> CloseRequested;
	}

	internal class CloseEventArgs : EventArgs
	{
		private CloseEventArgs( bool? result = null )
		{
			Result = result;
		}

		public bool? Result { get; }

		public static readonly CloseEventArgs Cancel = new CloseEventArgs( false );
		public static readonly CloseEventArgs Ok = new CloseEventArgs( true );
	}
}