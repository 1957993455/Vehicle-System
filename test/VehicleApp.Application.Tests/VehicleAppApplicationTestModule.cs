using VehicleApp.Application;
using VehicleApp.Domain.Tests;
using Volo.Abp.Modularity;

namespace VehicleApp.Application.Tests;

[DependsOn(
    typeof(VehicleAppApplicationModule),
    typeof(VehicleAppDomainTestModule)
)]
public class VehicleAppApplicationTestModule : AbpModule
{

}
