using Blitzy.PluginInterfaces;
using Ninject.Activation;

namespace Blitzy.Models.Db
{
	internal class DatabaseProvider : Provider<IDatabase>
	{
		/// <summary>
		/// Creates an instance within the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>
		/// The created instance.
		/// </returns>
		protected override IDatabase CreateInstance( IContext context )
		{
			return new Database( Constants.DatabaseFile );
		}
	}
}