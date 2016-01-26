using Ninject;

namespace Blitzy.Services
{
	interface IServiceRepository
	{
		ISettingsDialogService SettingsDialog { get; }
	}

	internal class ServiceRepository : IServiceRepository
	{
		[Inject]
		public ISettingsDialogService SettingsDialog { get; set; }
	}
}