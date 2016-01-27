using System;
using System.Windows.Data;
using Blitzy.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Blitzy.Tests.Converters
{
	[TestClass]
	public class ConverterChainTests
	{
		[TestMethod, TestCategory( "Converters" )]
		public void AllConvertersInChainAreCalled()
		{
			// Arrange
			var c1 = new Mock<IValueConverter>();
			c1.Setup( c => c.Convert( null, null, null, null ) ).Verifiable();
			var c2 = new Mock<IValueConverter>();
			c2.Setup( c => c.Convert( null, null, null, null ) ).Verifiable();
			var c3 = new Mock<IValueConverter>();
			c3.Setup( c => c.Convert( null, null, null, null ) ).Verifiable();

			var conv = new ConverterChain {c1.Object, c2.Object, c3.Object};

			// Act
			conv.Convert( null, null, null, null );

			// Assert
			c1.Verify( c => c.Convert( null, null, null, null ), Times.Once() );
			c2.Verify( c => c.Convert( null, null, null, null ), Times.Once() );
			c3.Verify( c => c.Convert( null, null, null, null ), Times.Once() );
		}

		[TestMethod, TestCategory( "Converters" )]
		public void ConvertBackThrows()
		{
			// Arrange
			var conv = new ConverterChain();

			// Act
			var ex = ExceptionAssert.Catch<NotSupportedException>( () => conv.ConvertBack( null, null, null, null ) );

			// Assert
			Assert.IsNotNull( ex );
		}

		[TestMethod, TestCategory( "Converters" )]
		public void ConvertedValueIsPassedBetweenConverters()
		{
			// Arrange
			var c1 = new Mock<IValueConverter>();
			c1.Setup( c => c.Convert( 1, null, null, null ) ).Returns( 2 );
			var c2 = new Mock<IValueConverter>();
			c2.Setup( c => c.Convert( 2, null, null, null ) ).Returns( 3 );
			var c3 = new Mock<IValueConverter>();
			c3.Setup( c => c.Convert( 3, null, null, null ) ).Returns( 4 );

			var conv = new ConverterChain {c1.Object, c2.Object, c3.Object};

			// Act
			var result = (int)conv.Convert( 1, null, null, null );

			// Assert
			Assert.AreEqual( 4, result );
		}
	}
}