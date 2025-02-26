using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.Vehicle;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using VehicleApp.Application.Contracts.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace VehicleApp.HttpApi.Controllers;

[RemoteService]
[Route("api/vehicle")]
[Authorize(VehicleAppPermissions.Vehicle.Default)]
public class VehicleController : VehicleAppController, IVehicleAppService
{
    private readonly IVehicleAppService _vehicleAppService;

    public VehicleController(IVehicleAppService vehicleAppService)
    {
        _vehicleAppService = vehicleAppService;
    }

    [HttpGet]
    public Task<PagedResultDto<VehicleDto>> GetListAsync([FromQuery] GetListVehicleInput input)
    {
        return _vehicleAppService.GetListAsync(input);
    }

    [HttpGet("{id}")]
    public Task<VehicleDto> GetAsync(Guid id)
    {
        return _vehicleAppService.GetAsync(id);
    }

    [Authorize(VehicleAppPermissions.Vehicle.Create)]
    [HttpPost]
    public Task<VehicleDto> CreateAsync(CreateVehicleDto input)
    {
        return _vehicleAppService.CreateAsync(input);
    }

    [Authorize(VehicleAppPermissions.Vehicle.Update)]
    [HttpPut("{id}")]
    public Task<VehicleDto> UpdateAsync(Guid id, UpdateVehicleDto input)
    {
        return _vehicleAppService.UpdateAsync(id, input);
    }

    [Authorize(VehicleAppPermissions.Vehicle.Delete)]
    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return _vehicleAppService.DeleteAsync(id);
    }

    [Authorize(VehicleAppPermissions.Vehicle.UpdateMileage)]
    [HttpPut("{id}/mileage")]
    public Task<VehicleDto> UpdateMileageAsync(Guid id, [FromBody] int newMileage)
    {
        return _vehicleAppService.UpdateMileageAsync(id, newMileage);
    }

    [Authorize(VehicleAppPermissions.Vehicle.ChangeStatus)]
    [HttpPut("{id}/status")]
    public Task<VehicleDto> ChangeStatusAsync(Guid id, [FromBody] VehicleStatus newStatus)
    {
        return _vehicleAppService.ChangeStatusAsync(id, newStatus);
    }


}