using VehicleApp.Application.Contracts;
using VehicleApp.EntityFrameworkCore.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace VehicleApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(VehicleAppEntityFrameworkCoreModule),
    typeof(VehicleAppApplicationContractsModule)
)]
public class VehicleAppDbMigratorModule : AbpModule
{
}
