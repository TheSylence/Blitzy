using System.Threading.Tasks;

namespace Blitzy.ViewModels
{
	interface ILoadCallback
	{
		Task OnLoad( object data );
	}
}