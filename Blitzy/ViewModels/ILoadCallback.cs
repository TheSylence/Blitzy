using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blitzy.ViewModels
{
	interface ILoadCallback
	{
		Task OnLoad( object data );
	}
}
