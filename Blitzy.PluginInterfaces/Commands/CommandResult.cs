using System;

namespace Blitzy.PluginInterfaces.Commands
{
	/// <summary>
	///     Result of a command execution
	/// </summary>
	public class CommandResult
	{
		/// <summary>
		///     Constructs a failed CommandResult.
		/// </summary>
		/// <param name="message">Error message</param>
		public CommandResult( string message )
			: this( false )
		{
			Message = message;
		}

		/// <summary>
		///     Constructs a failed CommandResult
		/// </summary>
		/// <param name="exception">The exception.</param>
		public CommandResult( Exception exception )
			: this( false )
		{
			Message = exception.ToString();
			Exception = exception;
		}

		private CommandResult( bool success )
		{
			IsSuccess = success;
		}

		/// <summary>
		///     A successful execution
		/// </summary>
		public static CommandResult Success { get; } = new CommandResult( true );

		/// <summary>
		///     The exception associated with this result, if any.
		/// </summary>
		public Exception Exception { get; }

		/// <summary>
		///     Indicates whether the exeuction was successful or not
		/// </summary>
		public bool IsSuccess { get; }

		/// <summary>
		///     Error message if the execution failed.
		/// </summary>
		public string Message { get; }
	}
}