using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blitzy.Utilities
{
	interface IAssembly
	{
		IEnumerable<Type> GetTypes();
	}

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