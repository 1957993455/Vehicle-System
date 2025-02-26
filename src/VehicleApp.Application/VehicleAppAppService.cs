using VehicleApp.Domain.Shared.Localization;
using Volo.Abp.Application.Services;

namespace VehicleApp.Application;

/* Inherit your application services from this class.
 */
public abstract class VehicleAppAppService : ApplicationService
{
    protected VehicleAppAppService()
    {
        LocalizationResource = typeof(VehicleAppResource);
    }
}
