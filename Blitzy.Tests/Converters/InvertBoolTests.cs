using System.Windows;
using Blitzy.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blitzy.Tests.Converters
{
	[TestClass]
	public class InvertBoolTests
	{
		[TestMethod, TestCategory( "Converters" )]
		public void ConvertBackInvertsBool()
		{
			// Arrange
			var conv = new InvertBool();

			// Act
			var invertedTrue = (bool)conv.ConvertBack( true, null, null, null );
			var invertedFalse = (bool)conv.ConvertBack( false, null, null, null );

			// Assert
			Assert.IsFalse( invertedTrue );
			Assert.IsTrue( invertedFalse );
		}

		[TestMethod, TestCategory( "Converters" )]
		public void ConvertInvertsBool()
		{
			// Arrange
			var conv = new InvertBool();

			// Act
			var invertedTrue = (bool)conv.Convert( true, null, null, null );
			var invertedFalse = (bool)conv.Convert( false, null, null, null );

			// Assert
			Assert.IsFalse( invertedTrue );
			Assert.IsTrue( invertedFalse );
		}

		[TestMethod, TestCategory( "Converters" )]
		public void WrongTypeResultsInUnsetValue()
		{
			// Arrange
			var conv = new InvertBool();

			// Act
			var result = conv.Convert( string.Empty, null, null, null );
			var backResult = conv.ConvertBack( string.Empty, null, null, null );

			// Assert
			Assert.AreEqual( DependencyProperty.UnsetValue, result );
			Assert.AreEqual( DependencyProperty.UnsetValue, backResult );
		}
	}
}