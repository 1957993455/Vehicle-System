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

            var vehicles = new List<VehicleAggregateRoot>
            {
                new VehicleAggregateRoot(Guid.NewGuid(), "BMW", "X5", "2019","黄色", "2.0T",  10000,"as",Guid.NewGuid(),Guid.NewGuid()),
                new VehicleAggregateRoot(Guid.NewGuid(), "Audi", "A4", "2018","白色", "2.5T",  15000,"as",Guid.NewGuid(),Guid.NewGuid()),
                new VehicleAggregateRoot(Guid.NewGuid(), "Mercedes", "C63", "2017","红色", "3.0T",  20000,"as",Guid.NewGuid(),Guid.NewGuid()),
                new VehicleAggregateRoot(Guid.NewGuid(), "Toyota", "Camry", "2016","蓝色", "2.0T",  12000,"as",Guid.NewGuid(),Guid.NewGuid()),
            };

            await vehicleRepository.InsertManyAsync(vehicles);
        }
    }
}
