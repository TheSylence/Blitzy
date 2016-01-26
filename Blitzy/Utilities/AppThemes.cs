using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Blitzy.ViewModels.Settings.Core;
using MahApps.Metro;

namespace Blitzy.Utilities
{
	internal interface IAppThemes
	{
		void ChangeStyle( string theme, string accent );

		IEnumerable<ColorItem> Accents { get; }
		IEnumerable<ColorItem> Themes { get; }
	}

	[ExcludeFromCodeCoverage]
	internal class AppThemes : IAppThemes
	{
		public void ChangeStyle( string themeName, string accentName )
		{
			if( string.IsNullOrWhiteSpace( themeName ) || string.IsNullOrWhiteSpace( accentName ) )
			{
				return;
			}

			var accent = ThemeManager.Accents.FirstOrDefault( a => a.Name == accentName );
			var theme = ThemeManager.AppThemes.FirstOrDefault( t => t.Name == themeName );

			ThemeManager.ChangeAppStyle( Application.Current, accent, theme );
		}

		public IEnumerable<ColorItem> Accents => ThemeManager.Accents.Select( a => new ColorItem
		{
			Name = a.Name,
			ColorBrush = a.Resources["AccentColorBrush"] as Brush
		} );

		public IEnumerable<ColorItem> Themes => ThemeManager.AppThemes.Select( t => new ColorItem
		{
			Name = t.Name,
			ColorBrush = t.Resources["WhiteColorBrush"] as Brush,
			BorderBrush = t.Resources["BlackColorBrush"] as Brush
		} );
	}
}