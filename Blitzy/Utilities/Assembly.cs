using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Blitzy.Utilities
{
	interface IAssembly
	{
		IEnumerable<Type> GetTypes();
	}

	[ExcludeFromCodeCoverage]
	internal class AssemblyWrapper : IAssembly
	{
		public AssemblyWrapper( Assembly wrapped )
		{
			Wrapped = wrapped;
		}

		public IEnumerable<Type> GetTypes()
		{
			return Wrapped.GetTypes();
		}

		private readonly Assembly Wrapped;
	}
}