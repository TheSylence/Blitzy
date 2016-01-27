using Anotar.NLog;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Blitzy
{
	// ReSharper disable once UnusedMember.Global
	[ExcludeFromCodeCoverage]
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