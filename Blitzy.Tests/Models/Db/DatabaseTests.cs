﻿using Blitzy.Models.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blitzy.Tests.Models.Db
{
	[TestClass]
	public class DatabaseTests
	{
		[TestMethod, TestCategory( "Models.Db" )]
		public async Task CleanupRemovesExpiredEntries()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					var ticksOld = DateTime.Now.AddDays( -1 ).Ticks;
					var ticksNew = DateTime.Now.AddDays( 1 ).Ticks;
					DatabaseHelper.NonQuery( $"INSERT INTO data (key, value, expires) VALUES ('key', 123, {ticksOld})", connection );
					DatabaseHelper.NonQuery( $"INSERT INTO data (key, value, expires) VALUES ('key1', 123, {ticksNew})", connection );

					// Act
					await db.Cleanup();

					// Assert
					var fromDb = DatabaseHelper.SelectSingle( "SELECT value FROM data WHERE key = 'key'", connection );
					Assert.IsNull( fromDb );

					fromDb = DatabaseHelper.SelectSingle( "SELECT value FROM data WHERE key = 'key1'", connection );
					Assert.IsNotNull( fromDb );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public void DatabaseFileCanBeOpenedAndClosed()
		{
			// Arrange
			using( new Database( Path.GetTempFileName() ) )
			{
				// Act

				// Assert
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task GetConvertsValue()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					DatabaseHelper.NonQuery( $"INSERT INTO data (key, value, expires) VALUES ('key', 123, {long.MaxValue})", connection );

					// Act
					int result = await db.Get<int>( "key" );

					// Assert
					Assert.AreEqual( 123, result );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task GetReturnsDefaultForNotFoundValueWhenConverting()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					// Act
					var fromDb = await db.Get<int>( "non_existing" );

					// Assert
					Assert.AreEqual( 0, fromDb );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task GetReturnsExistingValue()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					DatabaseHelper.NonQuery( $"INSERT INTO data (key, value, expires) VALUES ('key', 123, {long.MaxValue})", connection );

					// Act
					var result = await db.Get<object>( "key" );

					// Assert
					Assert.IsNotNull( result );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task GetReturnsNullForNonExistingKey()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					// Act
					var result = await db.Get<object>( "non existing" );

					// Assert
					Assert.IsNull( result );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public void InvalidFileNameThrows()
		{
			// Arrange
			string fileName = "???";

			// Act
			var ex = ExceptionAssert.Catch<ArgumentException>( () => new Database( fileName ) );

			// Assert
			Assert.IsNotNull( ex );
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task KeyExistanceIsCorrectlyRead()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					// Act
					var before = await db.KeyExists( "key" );

					DatabaseHelper.NonQuery( $"INSERT INTO data (key, value, expires) VALUES ('key', 123, {long.MaxValue})", connection );

					var after = await db.KeyExists( "key" );

					// Assert
					Assert.IsFalse( before );
					Assert.IsTrue( after );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task RemoveDeletesRecordFromDatabase()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					DatabaseHelper.NonQuery( $"INSERT INTO data (key, value, expires) VALUES ('key', 123, {long.MaxValue})", connection );

					// Act
					await db.Remove( "key" );

					// Assert
					var fromDb = DatabaseHelper.SelectSingle( "SELECT value FROM data WHERE key = 'key'", connection );
					Assert.IsNull( fromDb );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task RemovingNonExistingKeyDoesNothing()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					// Act
					var ex = await ExceptionAssert.Catch<Exception>( async () => await db.Remove( "key" ) );

					// Assert
					Assert.IsNull( ex );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task SetAddsNewEntry()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					// Act
					await db.Set( "newkey", 123 );

					// Assert
					var fromDb = DatabaseHelper.SelectSingle( "SELECT value FROM data WHERE key = 'newkey'", connection );
					Assert.AreEqual( "123", fromDb.ToString() );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task SetEditsExistingEntry()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					DatabaseHelper.NonQuery( $"INSERT INTO data (key, value, expires) VALUES ('key', 123, {long.MaxValue})", connection );

					// Act
					await db.Set( "key", 345 );

					// Assert
					var fromDb = DatabaseHelper.SelectSingle( "SELECT value FROM data WHERE key = 'key'", connection );
					Assert.AreEqual( "345", fromDb.ToString() );
				}
			}
		}

		[TestMethod, TestCategory( "Models.Db" )]
		public async Task SetUsesCorrectExpiryTime()
		{
			// Arrange
			using( var connection = DatabaseHelper.OpenMemoryConnection() )
			{
				using( var db = new Database( connection ) )
				{
					// Act
					await db.Set( "ex_now", 123, DateTime.Now );
					await db.Set( "no_ex", 123 );

					// Assert
					var fromDb = DatabaseHelper.SelectSingle( "SELECT expires FROM data WHERE key = 'no_ex'", connection );
					Assert.AreEqual( DateTime.MaxValue.Ticks, fromDb );

					fromDb = DatabaseHelper.SelectSingle( "SELECT expires FROM data WHERE key = 'ex_now'", connection );
					Assert.AreNotEqual( DateTime.MaxValue.Ticks, fromDb );
					Assert.AreNotEqual( DateTime.Now.Ticks, fromDb );
				}
			}
		}
	}
}