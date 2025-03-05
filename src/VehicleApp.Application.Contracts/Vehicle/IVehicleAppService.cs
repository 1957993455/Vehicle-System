using System;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Services;

namespace VehicleApp.Application.Contracts.Vehicle;

public interface IVehicleAppService : ICrudAppService<
    VehicleDto,
    Guid,
    GetListVehicleInput,
    CreateVehicleDto,
    UpdateVehicleDto>
{
    Task<VehicleDto> UpdateMileageAsync(Guid id, int newMileage);

    Task<VehicleDto> ChangeStatusAsync(Guid id, VehicleStatus newStatus);

    Task BatchDeleteAsync(Guid[] ids);
}
