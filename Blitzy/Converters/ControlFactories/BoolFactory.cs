using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Blitzy.Converters.ControlFactories
{
	internal class BoolFactory : IControlFactory
	{
		public bool CanCreate( Type propertyType )
		{
			return typeof( bool ) == propertyType;
		}

		public FrameworkElement CreateControl( object obj, PropertyInfo prop )
		{
			var element = new CheckBox();

			var binding = new Binding( prop.Name )
			{
				Source = obj,
				Mode = BindingMode.TwoWay
			};

			element.SetBinding( ToggleButton.IsCheckedProperty, binding );

			return element;
		}
	}
}