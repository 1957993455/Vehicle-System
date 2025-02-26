using System.Threading.Tasks;

namespace VehicleApp.Domain.Data;

public interface IVehicleAppDbSchemaMigrator
{
    Task MigrateAsync();
}
