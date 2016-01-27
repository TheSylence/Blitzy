using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Blitzy.PluginInterfaces.Commands;

namespace Blitzy.Windows
{
	internal class WindowsCommand : CommandNode
	{
		public WindowsCommand( CommandType type )
		{
			CommandType = type;
		}

		/// <summary>
		///     Executes this command asynchronously
		/// </summary>
		/// <param name="data">User entered data for this command.</param>
		/// <param name="primary">Flag indicating whether primary (true) or secondary (false) execution mode is used.</param>
		/// <returns>Result of the execution</returns>
		public override async Task<CommandResult> Execute( string data, bool primary )
		{
			var info = new ProcessStartInfo();

			switch( CommandType )
			{
			case CommandType.Shutdown:
				info.FileName = "shutdown";
				break;

			case CommandType.Restart:
				info.FileName = "shutdown";
				break;

			case CommandType.Logoff:
				info.FileName = "logoff";
				break;
			}

			return await Task.Run( () =>
			{
				var proc = Process.Start( info );
				if( proc == null )
				{
					throw new Exception( "Failed to start process" );
				}

				proc.WaitForExit();
			} ).ContinueWith( task =>
			{
				if( task.IsFaulted )
				{
					return new CommandResult( task.Exception );
				}

				return CommandResult.Success;
			} );
		}

		/// <summary>
		///     Description of this node
		/// </summary>
		public override string Description => CommandDescriptions[CommandType];

		/// <summary>
		///     Name of this node
		/// </summary>
		public override string Name => CommandNames[CommandType];

		private static readonly Dictionary<CommandType, string> CommandDescriptions = new Dictionary<CommandType, string>
		{
			{CommandType.Shutdown, "Shutdown the computer"},
			{CommandType.Restart, "Restart the computer"},
			{CommandType.Logoff, "Logoff the currently logged in user"}
		};

		private static readonly Dictionary<CommandType, string> CommandNames = new Dictionary<CommandType, string>
		{
			{CommandType.Shutdown, "Shutdown"},
			{CommandType.Restart, "Restart"},
			{CommandType.Logoff, "Logoff"}
		};

		private readonly CommandType CommandType;
	}
}