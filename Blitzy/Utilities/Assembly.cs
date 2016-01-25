using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blitzy.Utilities
{
	interface IAssembly
	{
		IEnumerable<Type> GetTypes();
	}

	class AssemblyWrapper : IAssembly
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
