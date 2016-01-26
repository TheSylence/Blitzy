using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Blitzy.Models;
using Blitzy.Models.Plugins;
using Blitzy.ViewModels.Settings.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Blitzy.ViewModels.Settings
{
	internal interface ISettingsDialogViewModel : IWindowController, ILoadCallback
	{
		ICommand CancelCommand { get; }
		ICommand SaveCommand { get; }
		ITreeViewItemViewModel SelectedItem { get; }
		ICollection<ITreeViewItemViewModel> TopLevelItems { get; }
	}

	internal class SettingsDialogViewModel : ObservableObject, ISettingsDialogViewModel
	{
		public SettingsDialogViewModel( ISettings settings, IPluginContainer pluginContainer )
		{
			Settings = settings;
			PluginContainer = pluginContainer;

			TopLevelItems = new ObservableCollection<ITreeViewItemViewModel>();
		}

		public event EventHandler<CloseEventArgs> CloseRequested;

		public async Task OnLoad( object data )
		{
			await Settings.Load();

			TopLevelItems.Add( new CoreSettingsViewModel( Settings ) );
			TopLevelItems.Add( new PluginListViewModel( Settings, PluginContainer ) );
			TopLevelItems.Add( new AboutViewModel() );

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
			[DebuggerStepThrough] get { return _SelectedItem; }
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
		private readonly IPluginContainer PluginContainer;
		private readonly ISettings Settings;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private RelayCommand _CancelCommand;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private RelayCommand _SaveCommand;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private ITreeViewItemViewModel _SelectedItem;
	}
}