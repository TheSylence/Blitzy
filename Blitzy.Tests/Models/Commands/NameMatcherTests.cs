using Blitzy.Models.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blitzy.Tests.Models.Commands
{
	[TestClass]
	public class NameMatcherTests
	{
		[TestMethod, TestCategory( "Models.Commands" )]
		public void CamelCasesAreMatched()
		{
			var command = "Visual Studio Command Prompt";

			Assert.IsTrue( NameMatcher.Matches( command, "vscp" ) );
			Assert.IsTrue( NameMatcher.Matches( command, "vsc" ) );
			Assert.IsFalse( NameMatcher.Matches( command, "vspc" ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void ContainingStringIsMatched()
		{
			var command = "This is a test";

			Assert.IsTrue( NameMatcher.Matches( command, "is" ) );
			Assert.IsTrue( NameMatcher.Matches( command, "this" ) );
			Assert.IsFalse( NameMatcher.Matches( command, "foo" ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void EmptyCommandIsNoMatch()
		{
			var input = "test";

			Assert.IsFalse( NameMatcher.Matches( null, input ) );
			Assert.IsFalse( NameMatcher.Matches( "", input ) );
			Assert.IsFalse( NameMatcher.Matches( " ", input ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void EmptyStringIsNoMatch()
		{
			var command = "Test";

			Assert.IsFalse( NameMatcher.Matches( command, null ) );
			Assert.IsFalse( NameMatcher.Matches( command, "" ) );
			Assert.IsFalse( NameMatcher.Matches( command, " " ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void NameWithoutSymbolsIsMatched()
		{
			var command = "Metallica - Nothing else matters";

			Assert.IsTrue( NameMatcher.Matches( command, "metallica nothing" ) );
			Assert.IsTrue( NameMatcher.Matches( command, "metallica nothing else matters test" ) );
			Assert.IsFalse( NameMatcher.Matches( command, "test" ) );
			Assert.IsFalse( NameMatcher.Matches( command, "metallica else test" ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void ShuffledWordsAreMatched()
		{
			var command = "Metallica - Nothing else matters";

			Assert.IsTrue( NameMatcher.Matches( command, "nothing else metallica" ) );
			Assert.IsTrue( NameMatcher.Matches( command, "else nothing metallica" ) );
			Assert.IsTrue( NameMatcher.Matches( command, "matters else" ) );
			Assert.IsTrue( NameMatcher.Matches( command, "nothing else" ) );
			Assert.IsFalse( NameMatcher.Matches( command, "nothing else test" ) );
		}
	}
}