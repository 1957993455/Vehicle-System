using VehicleApp.TestBase;
using Volo.Abp.Modularity;

namespace VehicleApp.Domain.Tests;

/* Inherit from this class for your domain layer tests. */
public abstract class VehicleAppDomainTestBase<TStartupModule> : VehicleAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
