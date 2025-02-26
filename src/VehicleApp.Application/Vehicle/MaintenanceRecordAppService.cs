using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Vehicle;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using VehicleApp.Domain.Vehicle;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Application.Vehicle;

public class MaintenanceRecordAppService(
    IRepository<MaintenanceRecordEntity, Guid> maintenanceRepository,
    IRepository<VehicleAggregateRoot, Guid> vehicleRepository)
    : ApplicationService, IMaintenanceRecordAppService
{
    public async Task<MaintenanceRecordDto> CreateAsync(CreateMaintenanceRecordDto input)
    {
        var vehicle = await vehicleRepository.GetAsync(input.VehicleId);
        var record = new MaintenanceRecordEntity
        {
            Description = input.Description,
            Cost = input.Cost,
            VehicleId = input.VehicleId
        };

        vehicle.AddMaintenanceRecord(record);
        await vehicleRepository.UpdateAsync(vehicle);

        return ObjectMapper.Map<MaintenanceRecordEntity, MaintenanceRecordDto>(record);
    }

    public async Task<List<MaintenanceRecordDto>> GetByVehicleIdAsync(Guid vehicleId)
    {
        var records = await maintenanceRepository.GetListAsync(x => x.VehicleId == vehicleId);
        return ObjectMapper.Map<List<MaintenanceRecordEntity>, List<MaintenanceRecordDto>>(records);
    }
}