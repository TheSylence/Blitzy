using System.Data.SQLite;

namespace Blitzy.Models.Db
{
	internal static class DatabaseExtensions
	{
		internal static void AddParameter( this SQLiteCommand command, string name, object value )
		{
			var p = command.CreateParameter();
			p.ParameterName = name;
			p.Value = value;

			command.Parameters.Add( p );
		}
	}
}