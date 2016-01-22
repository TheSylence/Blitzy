using System;
using System.Threading.Tasks;

namespace Blitzy.Tests
{
	public static class ExceptionAssert
	{
		public static async Task<TException> Catch<TException>( Func<Task> action ) where TException : Exception
		{
			TException catched = null;

			try
			{
				await action();
			}
			catch( TException ex )
			{
				catched = ex;
			}

			return catched;
		}

		public static TException Catch<TException>( Action action ) where TException : Exception
		{
			TException catched = null;

			try
			{
				action();
			}
			catch( TException ex )
			{
				catched = ex;
			}

			return catched;
		}
	}
}