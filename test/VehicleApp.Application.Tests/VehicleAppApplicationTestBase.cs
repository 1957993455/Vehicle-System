using VehicleApp.TestBase;
using Volo.Abp.Modularity;

namespace VehicleApp.Application.Tests;

public abstract class VehicleAppApplicationTestBase<TStartupModule> : VehicleAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
