using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace Blitzy.Models.Plugins
{
	partial class SchemaGenerator
	{
		public SchemaGenerator( SQLiteConnection connection )
		{
			Connection = connection;

			UpdateQueries = GenerateQueries().ToList();
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