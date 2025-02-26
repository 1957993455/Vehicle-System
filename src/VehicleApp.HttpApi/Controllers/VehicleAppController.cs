using VehicleApp.Domain.Shared.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace VehicleApp.HttpApi.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class VehicleAppController : AbpControllerBase
{
    protected VehicleAppController()
    {
        LocalizationResource = typeof(VehicleAppResource);
    }
}
