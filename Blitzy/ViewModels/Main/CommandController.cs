using Blitzy.Models;
using Blitzy.PluginInterfaces.Commands;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Anotar.NLog;

namespace Blitzy.ViewModels.Main
{
	internal interface ICommandController
	{
		Task ExecuteCommand( bool primary, string inputText );

		void SearchCommands( string inputText );

		ICollection<ICommandNode> Commands { get; }
		ICommandViewModel CurrentCommand { get; }
		int CurrentCommandIndex { get; set; }
	}

	internal class CommandController : ObservableObject, ICommandController
	{
		public CommandController( IInputProcessor processor, ISettings settings )
		{
			Processor = processor;
			Settings = settings;
		}

		private readonly IInputProcessor Processor;

		public async Task ExecuteCommand( bool primary, string inputText )
		{
			if( CurrentCommand == null )
			{
				return;
			}

			var commandData = Processor.ExtractCommandData( inputText );

			try
			{
				if( !CurrentCommand.Command.CanExecute( primary, commandData ) )
				{
					return;
				}

				await CurrentCommand.Command.Execute( commandData, primary ).ContinueWith( task =>
				{
					if( task.IsFaulted )
					{

					}
				} );
			}
			catch( Exception ex )
			{
				LogTo.WarnException( "Exception while executing command", ex );
			}
		}

		public void SearchCommands( string inputText )
		{
			throw new NotImplementedException();
		}

		public ICollection<ICommandNode> Commands { get; }

		public ICommandViewModel CurrentCommand
		{
			[DebuggerStepThrough]
			get
			{
				return _CurrentCommand;
			}
			set
			{
				if( _CurrentCommand == value )
				{
					return;
				}

				_CurrentCommand = value;
				RaisePropertyChanged();
			}
		}

		public int CurrentCommandIndex
		{
			[DebuggerStepThrough]
			get { return _CurrentCommandIndex; }
			set
			{
				if( _CurrentCommandIndex == value )
				{
					return;
				}

				_CurrentCommandIndex = value;

				if( Settings.ScrollThroughCommandList )
				{
					if( _CurrentCommandIndex < 0 )
					{
						_CurrentCommandIndex = Commands.Count - 1;
					}
					if( _CurrentCommandIndex >= Commands.Count )
					{
						_CurrentCommandIndex = 0;
					}
				}
				else
				{
					_CurrentCommandIndex = Math.Min( Math.Max( 0, _CurrentCommandIndex ), Commands.Count - 1 );
				}

				RaisePropertyChanged();
			}
		}

		private readonly ISettings Settings;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )]
		private ICommandViewModel _CurrentCommand;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )]
		private int _CurrentCommandIndex;
	}
}