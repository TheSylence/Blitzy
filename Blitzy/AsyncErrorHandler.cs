using Anotar.NLog;
using System;
using System.Diagnostics;

namespace Blitzy
{
	public static class AsyncErrorHandler
	{
		public static void HandleException( Exception exception )
		{
			LogTo.FatalException( "Exception in async code", exception );
			if( Debugger.IsAttached )
			{
				Debugger.Break();
			}
		}
	}
}