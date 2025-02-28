using VehicleApp.Domain.Shared.Enums;

namespace VehicleApp.Application.Contracts.Vehicle.Dtos;

/// <summary>
/// 更新车辆信息时使用的数据传输对象，包含了可以更新的车辆信息。
/// 用于在更新车辆的操作中传递需要修改的参数，通常只包含部分可修改的属性。
/// </summary>
public class UpdateVehicleDto
{
    /// <summary>
    /// 车辆行驶里程数，可在更新操作中修改。
    /// </summary>
    public int Mileage { get; set; }

    /// <summary>
    /// 车辆状态，如可用、维修中、已出租等，可在更新操作中修改。
    /// </summary>
    public VehicleStatus Status { get; set; }
}
