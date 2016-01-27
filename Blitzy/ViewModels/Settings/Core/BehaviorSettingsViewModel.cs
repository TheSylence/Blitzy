using System.Threading.Tasks;
using Blitzy.Models;
using Blitzy.Resources;

namespace Blitzy.ViewModels.Settings.Core
{
	internal class BehaviorSettingsViewModel : SettingsSectionViewModel
	{
		public BehaviorSettingsViewModel( ITreeViewItemViewModel parent, ISettings settings ) : base( parent, settings, Strings.Behavior )
		{
			CloseOnEscape = settings.CloseOnEscape;
			CloseOnFocusLost = settings.CloseOnFocusLost;
			CloseAfterExecution = settings.CloseAfterExecution;
			KeepInputContent = settings.KeepInputContent;

			ClearUnsavedChanges();
		}

		protected override Task OnSave()
		{
			Settings.CloseOnEscape = CloseOnEscape;
			Settings.CloseOnFocusLost = CloseOnFocusLost;
			Settings.CloseAfterExecution = CloseAfterExecution;
			Settings.KeepInputContent = KeepInputContent;

			return Task.CompletedTask;
		}

		public bool CloseAfterExecution
		{
			[System.Diagnostics.DebuggerStepThrough] get { return _CloseAfterExecution; }
			set
			{
				if( _CloseAfterExecution == value )
				{
					return;
				}

				_CloseAfterExecution = value;
				RaisePropertyChanged();
				MarkPropertyAsChanged( nameof( CloseAfterExecution ) );
			}
		}

		public bool CloseOnEscape
		{
			[System.Diagnostics.DebuggerStepThrough] get { return _CloseOnEscape; }
			set
			{
				if( _CloseOnEscape == value )
				{
					return;
				}

				_CloseOnEscape = value;
				RaisePropertyChanged();
				MarkPropertyAsChanged( nameof( CloseOnEscape ));
			}
		}

		public bool CloseOnFocusLost
		{
			[System.Diagnostics.DebuggerStepThrough] get { return _CloseOnFocusLost; }
			set
			{
				if( _CloseOnFocusLost == value )
				{
					return;
				}

				_CloseOnFocusLost = value;
				RaisePropertyChanged();
				MarkPropertyAsChanged( nameof( CloseOnFocusLost ) );
			}
		}

		public bool KeepInputContent
		{
			[System.Diagnostics.DebuggerStepThrough] get { return _KeepInputContent; }
			set
			{
				if( _KeepInputContent == value )
				{
					return;
				}

				_KeepInputContent = value;
				RaisePropertyChanged();
				MarkPropertyAsChanged( nameof( KeepInputContent ) );
			}
		}

		[System.Diagnostics.DebuggerBrowsable( System.Diagnostics.DebuggerBrowsableState.Never )] private bool _CloseAfterExecution;

		[System.Diagnostics.DebuggerBrowsable( System.Diagnostics.DebuggerBrowsableState.Never )] private bool _CloseOnEscape;

		[System.Diagnostics.DebuggerBrowsable( System.Diagnostics.DebuggerBrowsableState.Never )] private bool _CloseOnFocusLost;

		[System.Diagnostics.DebuggerBrowsable( System.Diagnostics.DebuggerBrowsableState.Never )] private bool _KeepInputContent;
	}
}