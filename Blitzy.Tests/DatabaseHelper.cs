using System.Data.SQLite;

namespace Blitzy.Tests
{
	internal static class DatabaseHelper
	{
		public static SQLiteConnection OpenMemoryConnection()
		{
			var sb = new SQLiteConnectionStringBuilder
			{
				DataSource = ":memory:"
			};

			var connection = new SQLiteConnection( sb.ToString() );

			return connection.OpenAndReturn();
		}

		public static int NonQuery( string query, SQLiteConnection connection )
		{
			using( var cmd = connection.CreateCommand() )
			{
				cmd.CommandText = query;

				return cmd.ExecuteNonQuery();
			}
		}

		public static object SelectSingle( string query, SQLiteConnection connection )
		{
			using( var cmd = connection.CreateCommand() )
			{
				cmd.CommandText = query;
				return cmd.ExecuteScalar();
			}
		}
	}
}