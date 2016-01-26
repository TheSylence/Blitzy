using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Blitzy.Models.Plugins;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Blitzy.ViewModels.Settings
{
	internal interface ISettingsDialogViewModel : IWindowController
	{
		ICommand CancelCommand { get; }
		ICommand SaveCommand { get; }
		ITreeViewItemViewModel SelectedItem { get; }
		ICollection<ITreeViewItemViewModel> TopLevelItems { get; }
	}

	internal class SettingsDialogViewModel : ObservableObject, ISettingsDialogViewModel
	{
		public event EventHandler<CloseEventArgs> CloseRequested;
		public SettingsDialogViewModel( IPluginContainer pluginContainer )
		{
			TopLevelItems = new ObservableCollection<ITreeViewItemViewModel>
			{
				new CoreSettingsViewModel(),
				new PluginListViewModel( pluginContainer ),
				new AboutViewModel()
			};

			foreach( var item in TopLevelItems )
			{
				item.IsExpanded = true;
				item.SelectionChanged += Item_SelectionChanged;
			}
		}

		private bool CanExecuteSaveCommand()
		{
			return true;
		}

		private void ExecuteCancelCommand()
		{
			CloseRequested?.Invoke( this, CloseEventArgs.Cancel );
		}

		private void ExecuteSaveCommand()
		{
			CloseRequested?.Invoke( this, CloseEventArgs.Ok );
		}

		private void Item_SelectionChanged( object sender, TreeViewSelectionEventArgs e )
		{
			SelectedItem = e.SelectedNode;
		}

		public ICommand CancelCommand => _CancelCommand ?? ( _CancelCommand = new RelayCommand( ExecuteCancelCommand ) );

		public ICommand SaveCommand => _SaveCommand ?? ( _SaveCommand = new RelayCommand( ExecuteSaveCommand, CanExecuteSaveCommand ) );

		public ITreeViewItemViewModel SelectedItem
		{
			[DebuggerStepThrough]
			get { return _SelectedItem; }
			set
			{
				if( _SelectedItem == value )
				{
					return;
				}

				_SelectedItem = value;
				RaisePropertyChanged();
			}
		}

		public ICollection<ITreeViewItemViewModel> TopLevelItems { get; }

		[DebuggerBrowsable( DebuggerBrowsableState.Never )]
		private RelayCommand _CancelCommand;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )]
		private RelayCommand _SaveCommand;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )]
		private ITreeViewItemViewModel _SelectedItem;
	}
}