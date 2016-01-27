using System;
using System.Diagnostics.CodeAnalysis;

namespace Blitzy.Utilities
{
	interface ITypeActivator
	{
		TType CreateInstance<TType>( Type type ) where TType : class;
	}

	[ExcludeFromCodeCoverage]
	internal class TypeActivator : ITypeActivator
	{
		public TType CreateInstance<TType>( Type type ) where TType : class
		{
			return (TType)Activator.CreateInstance( type );
		}
	}
}