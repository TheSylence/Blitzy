using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Blitzy.Models.Db
{
	partial class SchemaGenerator
	{
		public SchemaGenerator( SQLiteConnection connection )
		{
			Connection = connection;

			UpdateQueries = GenerateQueries().ToList();
		}

		private static string InsertInto( string table, Dictionary<string,string> keyValuePairs )
		{
			var sb = new StringBuilder();

			sb.Append( $"INSERT INTO {table} (" );
			sb.Append( string.Join( ",", keyValuePairs.Keys.Select( Escape ) ) );
			sb.Append( ") VALUES (" );
			sb.Append( string.Join( ",", keyValuePairs.Values.Select( Escape ) ) );
			sb.Append( ");" );

			return sb.ToString();
		}

		static string Escape( string value )
		{
			return $"\"{value}\"";
		}

		public void CreateSchema()
		{
			int version = ReadSchemaVersion();

			foreach( var querySet in UpdateQueries.Skip( version ) )
			{
				using( var tx = Connection.BeginTransaction() )
				{
					foreach( var query in querySet )
					{
						using( var cmd = Connection.CreateCommand() )
						{
							cmd.CommandText = query;
							cmd.ExecuteNonQuery();
						}
					}

					++version;
					SetSchemaVersion( version );

					tx.Commit();
				}
			}
		}

		private int ReadSchemaVersion()
		{
			using( var cmd = Connection.CreateCommand() )
			{
				cmd.CommandText = "PRAGMA user_version";

				return Convert.ToInt32( cmd.ExecuteScalar() );
			}
		}

		private void SetSchemaVersion( int version )
		{
			using( var cmd = Connection.CreateCommand() )
			{
				cmd.CommandText = $"PRAGMA user_version = {version};";

				cmd.ExecuteNonQuery();
			}
		}

		private readonly SQLiteConnection Connection;
		private readonly List<string[]> UpdateQueries;
	}
}