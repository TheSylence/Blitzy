using Anotar.NLog;
using System;
using System.Diagnostics;

namespace Blitzy
{
	// ReSharper disable once UnusedMember.Global
	public static class AsyncErrorHandler
	{
		// ReSharper disable once UnusedMember.Global
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