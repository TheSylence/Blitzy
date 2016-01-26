using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Media;
using Blitzy.Models;
using Blitzy.Resources;
using Blitzy.Utilities;

namespace Blitzy.ViewModels.Settings.Core
{
	[ExcludeFromCodeCoverage]
	internal class ColorItem
	{
		public Brush BorderBrush { get; set; }
		public Brush ColorBrush { get; set; }
		public string Name { get; set; }
	}

	internal class VisualSettingsViewModel : TreeViewItemViewModel
	{
		public VisualSettingsViewModel( ITreeViewItemViewModel parent, ISettings settings ) : base( parent, Strings.Visual )
		{
			// TODO: DI?
			Themes = new AppThemes();

			AvailableAccents = Themes.Accents.ToList();
			AvailableThemes = Themes.Themes.ToList();

			_SelectedTheme = AvailableThemes.FirstOrDefault( t => t.Name == settings.Theme );
			_SelectedAccent = AvailableAccents.FirstOrDefault( t => t.Name == settings.Accent );
		}

		public ICollection<ColorItem> AvailableAccents { get; }
		public ICollection<ColorItem> AvailableThemes { get; }

		public ColorItem SelectedAccent
		{
			[DebuggerStepThrough] get { return _SelectedAccent; }
			set
			{
				if( _SelectedAccent == value )
				{
					return;
				}

				_SelectedAccent = value;
				RaisePropertyChanged();

				Themes.ChangeStyle( SelectedTheme?.Name, SelectedAccent?.Name );
			}
		}

		public ColorItem SelectedTheme
		{
			[DebuggerStepThrough] get { return _SelectedTheme; }
			set
			{
				if( _SelectedTheme == value )
				{
					return;
				}

				_SelectedTheme = value;
				RaisePropertyChanged();

				Themes.ChangeStyle( SelectedTheme?.Name, SelectedAccent?.Name );
			}
		}

		private readonly IAppThemes Themes;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private ColorItem _SelectedAccent;

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private ColorItem _SelectedTheme;
	}
}