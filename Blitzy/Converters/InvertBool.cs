using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Blitzy.Converters
{
	internal class InvertBool : IValueConverter
	{
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			return ConvertInternal( value, targetType, parameter, culture );
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			return ConvertInternal( value, targetType, parameter, culture );
		}

		static object ConvertInternal( object value, Type targetType, object parameter, CultureInfo culture )
		{
			try
			{
				bool b = (bool)value;

				return !b;
			}
			catch( InvalidCastException )
			{
				return DependencyProperty.UnsetValue;
			}
		}
	}
}