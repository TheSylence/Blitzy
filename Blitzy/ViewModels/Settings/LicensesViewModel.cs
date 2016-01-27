using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Blitzy.Resources;

namespace Blitzy.ViewModels.Settings
{
	internal class LicenseItem
	{
		public LicenseItem( string resourceName )
		{
			using( var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream( resourceName ) )
			{
				Debug.Assert( stream != null, "stream != null" );
				using( var reader = new StreamReader( stream, Encoding.UTF8, false, 1024, true ) )
				{
					Text = reader.ReadToEnd();
				}
			}

			string str = resourceName.Substring( "Blitzy.Resources.Licenses.".Length );
			Name = str.Substring( 0, str.Length - ".txt".Length );
		}

		public string Name { get; }
		public string Text { get; }
	}

	internal class LicensesViewModel : TreeViewItemViewModel
	{
		public LicensesViewModel( ITreeViewItemViewModel parent ) : base( parent, Strings.Licenses )
		{
			Licenses = Assembly.GetExecutingAssembly().GetManifestResourceNames()
				.Where( n => n.StartsWith( "Blitzy.Resources.Licenses." ) )
				.Select( r => new LicenseItem( r ) ).ToList();
		}

		public ICollection<LicenseItem> Licenses { get; }

		public LicenseItem SelectedLicense
		{
			[DebuggerStepThrough] get { return _SelectedLicense; }
			set
			{
				if( _SelectedLicense == value )
				{
					return;
				}

				_SelectedLicense = value;
				RaisePropertyChanged();
			}
		}

		[DebuggerBrowsable( DebuggerBrowsableState.Never )] private LicenseItem _SelectedLicense;
	}
}