using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using Blitzy.PluginInterfaces;

namespace Blitzy.Models.Db
{
	internal class Database : IDatabase, IDisposable
	{
		public Database( string fileName )
		{
			var sb = new SQLiteConnectionStringBuilder
			{
				DataSource = fileName,
				JournalMode = SQLiteJournalModeEnum.Wal
			};

			Connection = new SQLiteConnection( sb.ToString() );
			Connection.Open();
			DisposeConnection = true;

			new SchemaGenerator( Connection ).CreateSchema();
		}

		public Database( SQLiteConnection connection )
		{
			Connection = connection;
			DisposeConnection = false;

			new SchemaGenerator( Connection ).CreateSchema();
		}

		public async Task Cleanup()
		{
			using( var cmd = Connection.CreateCommand() )
			{
				cmd.CommandText = "DELETE FROM data WHERE expires < @expiry";
				cmd.AddParameter( "expiry", DateTime.Now.Ticks );

				await cmd.ExecuteNonQueryAsync();
			}
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		/// <summary>
		///     Retrieves a value from the database
		/// </summary>
		/// <typeparam name="TResult">Type to read the value as</typeparam>
		/// <param name="key">Key of the entry to read</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		public async Task<TResult> Get<TResult>( string key )
		{
			using( var cmd = Connection.CreateCommand() )
			{
				cmd.CommandText = "SELECT value FROM data WHERE key = @key AND expires >= @expiry";
				cmd.AddParameter( "expiry", DateTime.Now.Ticks );
				cmd.AddParameter( "key", key );

				object result = await cmd.ExecuteScalarAsync();

				if( result == null || DBNull.Value.Equals( result ) )
				{
					return default(TResult);
				}

				return (TResult)Convert.ChangeType( result, typeof( TResult ) );
			}
		}

		/// <summary>
		///     Checks if a key exisits in the database
		/// </summary>
		/// <param name="key">The key to check</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		public async Task<bool> KeyExists( string key )
		{
			using( var cmd = Connection.CreateCommand() )
			{
				cmd.CommandText = "SELECT expires FROM data WHERE key = @key";
				cmd.AddParameter( "key", key );

				var result = await cmd.ExecuteScalarAsync();
				return result != null && !DBNull.Value.Equals( result );
			}
		}

		/// <summary>
		///     Removes a key (and its value) from the database
		/// </summary>
		/// <param name="key">Key to remove</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		public async Task Remove( string key )
		{
			using( var cmd = Connection.CreateCommand() )
			{
				cmd.CommandText = "DELETE FROM data WHERE key = @key";
				cmd.AddParameter( "key", key );

				await cmd.ExecuteNonQueryAsync();
			}
		}

		/// <summary>
		///     Adds or updates an entry in the database
		/// </summary>
		/// <param name="key">Key of the entry to add</param>
		/// <param name="value">Value of the entry</param>
		/// <param name="expires">Optionally specify when this entry expires</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		public async Task Set( string key, object value, DateTime? expires = null )
		{
			using( var cmd = Connection.CreateCommand() )
			{
				cmd.CommandText = "INSERT OR REPLACE INTO data (key, value, expires) VALUES( @key, @value, @expires );";
				cmd.AddParameter( "key", key );
				cmd.AddParameter( "value", value );
				cmd.AddParameter( "expires", ( expires ?? DateTime.MaxValue ).Ticks );

				await cmd.ExecuteNonQueryAsync();
			}
		}

		private void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( DisposeConnection )
				{
					Connection.Dispose();
				}
			}
		}

		private readonly SQLiteConnection Connection;
		private readonly bool DisposeConnection;
	}
}