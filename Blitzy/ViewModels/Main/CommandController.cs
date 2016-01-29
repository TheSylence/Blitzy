using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Anotar.NLog;
using Blitzy.Models;
using GalaSoft.MvvmLight;

namespace Blitzy.ViewModels.Main
{
	internal interface ICommandController
	{
		Task ExecuteCommand( bool primary, string inputText );

		void SearchCommands( string inputText );

		ICollection<ICommandViewModel> Commands { get; }
		ICommandViewModel CurrentCommand { get; }
		int CurrentCommandIndex { get; set; }
	}

	internal class CommandController : ObservableObject, ICommandController
	{
		public CommandController( IInputProcessor processor, ISettings settings )
		{
			Processor = processor;
			Settings = settings;

			CommandList = new ObservableCollection<ICommandViewModel>();
		}

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
						// TODO: Notify
					}
				} );
			}
			catch( Exception ex )
			{
				LogTo.WarnException( "Exception while executing command", ex );
				// TODO: Notify
			}
		}

		public void SearchCommands( string inputText )
		{
			var oldSelected = CurrentCommand;

			Commands.Clear();
			CurrentCommand = null;
			_CurrentCommandIndex = -1;

			var matched = Processor.MatchedCommands( inputText, oldSelected?.Command );
			foreach( var cmd in matched )
			{
				CommandList.Add( new CommandViewModel( cmd ) );
			}

			int newIndex = CommandList.IndexOf( oldSelected );
			if( newIndex != -1 )
			{
				CurrentCommand = oldSelected;
			}
			else
			{
				CurrentCommand = CommandList.FirstOrDefault();
			}
			_CurrentCommandIndex = CommandList.IndexOf( CurrentCommand );

			RaisePropertyChanged( nameof( CurrentCommandIndex ) );
		}

		public ICollection<ICommandViewModel> Commands => CommandList;

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

		private readonly ObservableCollection<ICommandViewModel> CommandList;
		private readonly IInputProcessor Processor;
		private readonly ISettings Settings;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )]
		private ICommandViewModel _CurrentCommand;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )]
		private int _CurrentCommandIndex;
	}
}