using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class VehicleAppDbContextFactory : IDesignTimeDbContextFactory<VehicleAppDbContext>
{
    public VehicleAppDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        VehicleAppEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<VehicleAppDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new VehicleAppDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../VehicleApp.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
