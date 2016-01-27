using System;
using System.Reflection;
using System.Windows;

namespace Blitzy.Converters.ControlFactories
{
	interface IControlFactory
	{
		bool CanCreate( Type propertyType );

		FrameworkElement CreateControl( object obj, PropertyInfo prop );
	}
}