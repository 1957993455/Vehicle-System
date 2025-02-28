using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Domain.Vehicle
{
    public class VehicleDataSeedContributor(IRepository<VehicleAggregateRoot, Guid> vehicleRepository) : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await vehicleRepository.GetCountAsync() > 0)
                return;

            List<VehicleAggregateRoot> vehicles = new List<VehicleAggregateRoot>();

            for (int i = 1; i <= 20; i++)
            {
                Guid vehicleId = Guid.NewGuid();
                string vin = $"VIN{i:D17}";
                string make = i % 2 == 0 ? "Toyota" : "BMW";
                string model = i % 2 == 0 ? "Corolla" : "X5";
                string year = (2010 + i % 10).ToString();
                string color = i % 3 == 0 ? "Red" : (i % 3 == 1 ? "Blue" : "Black");
                int mileage = i * 1000;
                string licensePlate = $"ABC-{i:D3}";
                Guid storeId = Guid.NewGuid();
                Guid ownerId = Guid.NewGuid();

                VehicleAggregateRoot vehicle = new VehicleAggregateRoot(vehicleId, vin, make, model, year, color, mileage, licensePlate, storeId, ownerId);
                vehicles.Add(vehicle);
            }

            await vehicleRepository.InsertManyAsync(vehicles);
        }
    }
}
