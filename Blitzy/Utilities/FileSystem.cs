using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Blitzy.Utilities
{
	interface IFileSystem
	{
		IEnumerable<string> ListFiles( string path, string pattern, SearchOption options = SearchOption.AllDirectories );

		IAssembly LoadAssemblyFromFile( string fileName );
	}

	internal class FileSystem : IFileSystem
	{
		public IAssembly LoadAssemblyFromFile( string fileName )
		{
			return new AssemblyWrapper( Assembly.LoadFile( fileName ) );
		}

		public IEnumerable<string> ListFiles( string path, string pattern, SearchOption options = SearchOption.AllDirectories )
		{
			return Directory.GetFiles( path, pattern, options );
		}
	}
}