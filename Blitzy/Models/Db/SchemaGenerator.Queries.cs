using System.Collections.Generic;
using System.Linq;

namespace Blitzy.Models.Plugins
{
	partial class SchemaGenerator
	{
		private IEnumerable<string[]> GenerateQueries()
		{
			yield return S00().ToArray();
		}

		private static IEnumerable<string> S00()
		{
			yield return "CREATE TABLE data ( key TEXT PRIMARY KEY NOT NULL, value TEXT NOT NULL, expires INTEGER NOT NULL );";
		}
	}
}