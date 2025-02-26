using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehicleApp.Domain.Data;
using Volo.Abp.DependencyInjection;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore;

public class EntityFrameworkCoreVehicleAppDbSchemaMigrator(IServiceProvider serviceProvider)
        : IVehicleAppDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the VehicleAppDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<VehicleAppDbContext>()
            .Database
            .MigrateAsync();
    }
}