using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace VehicleApp.Domain.Data;

/* This is used if database provider does't define
 * IVehicleAppDbSchemaMigrator implementation.
 */
public class NullVehicleAppDbSchemaMigrator : IVehicleAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
