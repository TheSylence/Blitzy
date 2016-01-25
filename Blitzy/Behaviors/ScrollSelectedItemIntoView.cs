using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace Blitzy.Behaviors
{
	[ExcludeFromCodeCoverage]
	internal class ScrollSelectedItemIntoView : Behavior<ListBox>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			var dpd = DependencyPropertyDescriptor.FromProperty( Selector.SelectedIndexProperty, typeof( ListView ) );
			dpd?.AddValueChanged( AssociatedObject, delegate { HandleSelectionChanged(); } );

			dpd = DependencyPropertyDescriptor.FromProperty( Selector.SelectedItemProperty, typeof( ListView ) );
			dpd?.AddValueChanged( AssociatedObject, delegate { HandleSelectionChanged(); } );
		}

		private void HandleSelectionChanged()
		{
			AssociatedObject.ScrollIntoView( AssociatedObject.SelectedItem );
		}
	}
}