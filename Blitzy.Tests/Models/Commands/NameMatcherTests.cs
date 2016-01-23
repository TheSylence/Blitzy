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
			var matcher = new NameMatcher();
			var command = "Visual Studio Command Prompt";

			Assert.IsTrue( matcher.Matches( command, "vscp" ) );
			Assert.IsTrue( matcher.Matches( command, "vsc" ) );
			Assert.IsFalse( matcher.Matches( command, "vspc" ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void ContainingStringIsMatched()
		{
			var matcher = new NameMatcher();
			var command = "This is a test";

			Assert.IsTrue( matcher.Matches( command, "is" ) );
			Assert.IsTrue( matcher.Matches( command, "this" ) );
			Assert.IsFalse( matcher.Matches( command, "foo" ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void EmptyCommandIsNoMatch()
		{
			var matcher = new NameMatcher();
			var input = "test";

			Assert.IsFalse( matcher.Matches( null, input ) );
			Assert.IsFalse( matcher.Matches( "", input ) );
			Assert.IsFalse( matcher.Matches( " ", input ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void EmptyStringIsNoMatch()
		{
			var matcher = new NameMatcher();
			var command = "Test";

			Assert.IsFalse( matcher.Matches( command, null ) );
			Assert.IsFalse( matcher.Matches( command, "" ) );
			Assert.IsFalse( matcher.Matches( command, " " ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void NameWithoutSymbolsIsMatched()
		{
			var matcher = new NameMatcher();
			var command = "Metallica - Nothing else matters";

			Assert.IsTrue( matcher.Matches( command, "metallica nothing" ) );
			Assert.IsTrue( matcher.Matches( command, "metallica nothing else matters test" ) );
			Assert.IsFalse( matcher.Matches( command, "test" ) );
			Assert.IsFalse( matcher.Matches( command, "metallica else test" ) );
		}

		[TestMethod, TestCategory( "Models.Commands" )]
		public void ShuffledWordsAreMatched()
		{
			var matcher = new NameMatcher();
			var command = "Metallica - Nothing else matters";

			Assert.IsTrue( matcher.Matches( command, "nothing else metallica" ) );
			Assert.IsTrue( matcher.Matches( command, "else nothing metallica" ) );
			Assert.IsTrue( matcher.Matches( command, "matters else" ) );
			Assert.IsTrue( matcher.Matches( command, "nothing else" ) );
			Assert.IsFalse( matcher.Matches( command, "nothing else test" ) );
		}
	}
}