using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Blitzy.Converters.ControlFactories;

namespace Blitzy.Converters
{
	internal class SettingsEditor : IValueConverter
	{
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			var grid = new Grid();
			grid.ColumnDefinitions.Add( new ColumnDefinition {Width = GridLength.Auto} );
			grid.ColumnDefinitions.Add( new ColumnDefinition() );

			var type = value.GetType();

			var properties = type.GetProperties( BindingFlags.Instance | BindingFlags.Public );
			foreach( var prop in properties.Where( IsBrowsableProperty ) )
			{
				grid.RowDefinitions.Add( new RowDefinition {Height = GridLength.Auto} );
				int rowIndex = grid.RowDefinitions.Count - 1;

				string name = GetDisplayName( prop );
				var txtDisplay = new TextBlock
				{
					Text = name,
					VerticalAlignment = VerticalAlignment.Center,
					Margin = new Thickness( 5 )
				};
				txtDisplay.SetValue( Grid.ColumnProperty, 0 );
				txtDisplay.SetValue( Grid.RowProperty, rowIndex );
				grid.Children.Add( txtDisplay );

				var element = CreateControl( value, prop );

				element.SetValue( Grid.ColumnProperty, 1 );
				element.SetValue( Grid.RowProperty, rowIndex );
				grid.Children.Add( element );
			}

			return grid;
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}

		private static IEnumerable<IControlFactory> RegisterControlFactories()
		{
			yield return new BoolFactory();
		}

		private FrameworkElement CreateControl( object obj, PropertyInfo prop )
		{
			var factory = ControlFactories.FirstOrDefault( f => f.CanCreate( prop.PropertyType ) );

			Debug.Assert( factory != null, "factory != null" );
			return factory.CreateControl( obj, prop );
		}

		private string GetDisplayName( PropertyInfo prop )
		{
			var attr = prop.GetCustomAttribute<DisplayNameAttribute>();

			return attr?.DisplayName ?? prop.Name;
		}

		private bool IsBrowsableProperty( PropertyInfo prop )
		{
			if( !prop.CanWrite )
			{
				return false;
			}
			if( !prop.SetMethod.IsPublic )
			{
				return false;
			}

			var attr = prop.GetCustomAttribute<BrowsableAttribute>();
			if( attr?.Browsable == false )
			{
				return false;
			}

			return true;
		}

		private static IEnumerable<IControlFactory> ControlFactories => _ControlFactories ?? ( _ControlFactories = RegisterControlFactories().ToList() );
		private static List<IControlFactory> _ControlFactories;
	}
}