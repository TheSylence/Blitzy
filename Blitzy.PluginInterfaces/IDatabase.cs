using System;
using System.Threading.Tasks;

namespace Blitzy.PluginInterfaces
{
	/// <summary>
	/// Interface for asynchronously accessing the global database where settings and data can be saved
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		/// Adds or updates an entry in the database
		/// </summary>
		/// <param name="key">Key of the entry to add</param>
		/// <param name="value">Value of the entry</param>
		/// <param name="expires">Optionally specify when this entry expires</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task Set( string key, object value, DateTime? expires = null );

		/// <summary>
		/// Retrieves a value from the database
		/// </summary>
		/// <typeparam name="TResult">Type to read the value as</typeparam>
		/// <param name="key">Key of the entry to read</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task<TResult> Get<TResult>( string key );

		/// <summary>
		/// Checks if a key exisits in the database
		/// </summary>
		/// <param name="key">The key to check</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task<bool> KeyExists( string key );

		/// <summary>
		/// Removes a key (and its value) from the database
		/// </summary>
		/// <param name="key">Key to remove</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task Remove( string key );
	}
}