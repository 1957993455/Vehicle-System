using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.Vehicle;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using Volo.Abp;
using VehicleApp.Application.Contracts.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace VehicleApp.HttpApi.Controllers;

[RemoteService]
[Route("api/maintenance-record")]
[Authorize(VehicleAppPermissions.MaintenanceRecord.Default)]
public class MaintenanceRecordController : VehicleAppController
{
    private readonly IMaintenanceRecordAppService _maintenanceRecordAppService;

    public MaintenanceRecordController(IMaintenanceRecordAppService maintenanceRecordAppService)
    {
        _maintenanceRecordAppService = maintenanceRecordAppService;
    }

    [Authorize(VehicleAppPermissions.MaintenanceRecord.Create)]
    [HttpPost]
    public Task<MaintenanceRecordDto> CreateAsync(CreateMaintenanceRecordDto input)
    {
        return _maintenanceRecordAppService.CreateAsync(input);
    }

    [Authorize(VehicleAppPermissions.MaintenanceRecord.View)]
    [HttpGet("vehicle/{vehicleId}")]
    public Task<List<MaintenanceRecordDto>> GetByVehicleIdAsync(Guid vehicleId)
    {
        return _maintenanceRecordAppService.GetByVehicleIdAsync(vehicleId);
    }
}