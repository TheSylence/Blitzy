using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Blitzy.ViewModels.Main
{
	internal interface IMainViewModel
	{
		ICommandController CommandController { get; }
		IInputProcessor InputProcessor { get; }
		string InputText { get; set; }
		ICommand SettingsCommand { get; }
	}

	internal class MainViewModel : ViewModelBase, IMainViewModel
	{
		public MainViewModel( ICommandController controller, IInputProcessor processor )
		{
			CommandController = controller;
			InputProcessor = processor;
		}

		private void ExecuteSettingsCommand()
		{
		}

		public ICommandController CommandController { get; }

		public IInputProcessor InputProcessor { get; }

		public string InputText
		{
			[DebuggerStepThrough]
			get
			{
				return _InputText;
			}
			set
			{
				if( _InputText == value )
				{
					return;
				}

				_InputText = value;
				RaisePropertyChanged();
				CommandController.SearchCommands( InputText );
			}
		}

		public ICommand SettingsCommand => _SettingsCommand ?? ( _SettingsCommand = new RelayCommand( ExecuteSettingsCommand ) );

		[DebuggerBrowsable( DebuggerBrowsableState.Never )]
		private string _InputText;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )]
		private RelayCommand _SettingsCommand;
	}
}