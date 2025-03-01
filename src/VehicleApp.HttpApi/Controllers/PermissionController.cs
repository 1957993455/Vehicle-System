using Volo.Abp.PermissionManagement;

namespace VehicleApp.HttpApi.Controllers;

public class PermissionController : VehicleAppController
{
    protected IPermissionAppService PermissionAppService { get; }

    public PermissionController(IPermissionAppService permissionAppService) => PermissionAppService = permissionAppService;
}
