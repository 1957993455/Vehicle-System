using VehicleApp.Domain;
using VehicleApp.TestBase;
using Volo.Abp.Modularity;

namespace VehicleApp.Domain.Tests;

[DependsOn(
    typeof(VehicleAppDomainModule),
    typeof(VehicleAppTestBaseModule)
)]
public class VehicleAppDomainTestModule : AbpModule
{

}
