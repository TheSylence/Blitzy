using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Blitzy.Models;
using Blitzy.Resources;
using GalaSoft.MvvmLight.CommandWpf;

namespace Blitzy.ViewModels.Settings.Core
{
	internal class UpdateSettingsViewModel : SettingsSectionViewModel
	{
		public UpdateSettingsViewModel( ITreeViewItemViewModel parent, ISettings settings ) : base( parent, settings, Strings.Updates )
		{
			EnableUpdates = settings.CheckForUpdates;
			IncludeBetaReleases = settings.PreviewUpdates;

			ClearUnsavedChanges();
		}

		protected override Task OnSave()
		{
			Settings.CheckForUpdates = EnableUpdates;
			Settings.PreviewUpdates = IncludeBetaReleases;

			return Task.CompletedTask;
		}

		private void ExecuteCheckForUpdatesCommand()
		{
		}

		public ICommand CheckForUpdatesCommand => _CheckForUpdatesCommand ?? ( _CheckForUpdatesCommand = new RelayCommand( ExecuteCheckForUpdatesCommand ) );

		public bool EnableUpdates
		{
			[System.Diagnostics.DebuggerStepThrough] get { return _EnableUpdates; }
			set
			{
				if( _EnableUpdates == value )
				{
					return;
				}

				_EnableUpdates = value;
				RaisePropertyChanged();
				MarkPropertyAsChanged( nameof( EnableUpdates ) );
			}
		}

		public bool IncludeBetaReleases
		{
			[System.Diagnostics.DebuggerStepThrough] get { return _IncludeBetaReleases; }
			set
			{
				if( _IncludeBetaReleases == value )
				{
					return;
				}

				_IncludeBetaReleases = value;
				RaisePropertyChanged();
				MarkPropertyAsChanged( nameof( IncludeBetaReleases ) );
			}
		}

		public DateTime LastUpdateCheck
		{
			[System.Diagnostics.DebuggerStepThrough] get { return _LastUpdateCheck; }
			set
			{
				if( _LastUpdateCheck == value )
				{
					return;
				}

				_LastUpdateCheck = value;
				RaisePropertyChanged();
			}
		}

		private RelayCommand _CheckForUpdatesCommand;

		[System.Diagnostics.DebuggerBrowsable( System.Diagnostics.DebuggerBrowsableState.Never )] private bool _EnableUpdates;

		[System.Diagnostics.DebuggerBrowsable( System.Diagnostics.DebuggerBrowsableState.Never )] private bool _IncludeBetaReleases;

		[System.Diagnostics.DebuggerBrowsable( System.Diagnostics.DebuggerBrowsableState.Never )] private DateTime _LastUpdateCheck;
	}
}