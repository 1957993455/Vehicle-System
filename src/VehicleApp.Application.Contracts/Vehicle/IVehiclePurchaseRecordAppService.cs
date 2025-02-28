using System;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using Volo.Abp.Application.Services;

namespace VehicleApp.Application.Contracts.Vehicle;

/// <summary>
/// 车辆购买记录应用服务接口
/// </summary>
public interface IVehiclePurchaseRecordAppService : ICrudAppService<
    VehiclePurchaseRecordDto,
    Guid,
    GetVehiclePurchaseRecordListInput,
    CreateVehiclePurchaseRecordDto,
    UpdateVehiclePurchaseRecordDto>
{
}
