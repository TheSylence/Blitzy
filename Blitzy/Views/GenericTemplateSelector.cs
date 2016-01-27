using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Blitzy.Views
{
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	[ContentProperty( "Template" )]
	public class GenericTemplateSelectorItem
	{
		public DataTemplate Template { get; set; }

		public Type TemplatedType { get; set; }
	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	[ContentProperty( "Templates" )]
	public class GenericTemplateSelector : DataTemplateSelector
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0",
			Justification = "Bullshit" )]
		public override DataTemplate SelectTemplate( object item, DependencyObject container )
		{
			if( item == null )
			{
				return null;
			}

			return Templates
				.Where( tpl => tpl.TemplatedType != null && IsDerived( item.GetType(), tpl.TemplatedType ) )
				.Select( tpl => tpl.Template ).FirstOrDefault();
		}

		private static bool IsDerived( Type itemType, Type baseType )
		{
			if( itemType == baseType )
			{
				return true;
			}

			if( itemType.BaseType == null )
			{
				return false;
			}

			return IsDerived( itemType.BaseType, baseType );
		}

		// ReSharper disable once CollectionNeverUpdated.Global
		public Collection<GenericTemplateSelectorItem> Templates { get; set; } = new Collection<GenericTemplateSelectorItem>();
	}
}