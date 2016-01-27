using System.Collections.Generic;
using System.Threading.Tasks;
using Blitzy.Models;

namespace Blitzy.ViewModels.Settings
{
	interface ISettingsSectionViewModel
	{
		Task Save();

		int UnsavedChanges { get; }
	}

	internal abstract class SettingsSectionViewModel : TreeViewItemViewModel, ISettingsSectionViewModel
	{
		protected SettingsSectionViewModel( ITreeViewItemViewModel parent, ISettings settings, string title, bool lazyLoadChildren = false )
			: base( parent, title, lazyLoadChildren )
		{
			Settings = settings;
		}

		public async Task Save()
		{
			await OnSave();
			ClearUnsavedChanges();
		}

		protected void ClearUnsavedChanges()
		{
			ChangedProperties.Clear();
		}

		protected void MarkPropertyAsChanged( string propertyName )
		{
			ChangedProperties.Add( propertyName );
		}

		protected abstract Task OnSave();

		public int UnsavedChanges => ChangedProperties.Count;

		protected ISettings Settings { get; }
		private readonly HashSet<string> ChangedProperties = new HashSet<string>();
	}
}