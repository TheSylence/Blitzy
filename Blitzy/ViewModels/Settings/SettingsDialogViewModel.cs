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
		bool HasUnsavedChanges { get; }
		ICommand SaveCommand { get; }
		ITreeViewItemViewModel SelectedItem { get; }
		ICollection<ITreeViewItemViewModel> TopLevelItems { get; }
		int UnsavedChanges { get; }
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

		int CalculateUnsavedChanges( ITreeViewItemViewModel parent = null )
		{
			var items = parent != null ? parent.Children : TopLevelItems;

			int count = 0;
			foreach( var item in items )
			{
				var section = item as ISettingsSectionViewModel;
				if( section != null )
				{
					count += section.UnsavedChanges;
				}

				count += CalculateUnsavedChanges( item );
			}

			return count;
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
			UnsavedChanges = CalculateUnsavedChanges();

			SelectedItem = e.SelectedNode;
		}

		public ICommand CancelCommand => _CancelCommand ?? ( _CancelCommand = new RelayCommand( ExecuteCancelCommand ) );

		public bool HasUnsavedChanges => UnsavedChanges > 0;

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

		public int UnsavedChanges
		{
			[DebuggerStepThrough] get { return _UnsavedChanges; }
			set
			{
				if( _UnsavedChanges == value )
				{
					return;
				}

				_UnsavedChanges = value;
				RaisePropertyChanged();
				RaisePropertyChanged( nameof( HasUnsavedChanges ) );
			}
		}

		private readonly IPluginContainer PluginContainer;

		private readonly ISettings Settings;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private RelayCommand _CancelCommand;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private RelayCommand _SaveCommand;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private ITreeViewItemViewModel _SelectedItem;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private int _UnsavedChanges;
	}
}