using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Vehicle.Dtos;

/// <summary>
/// 获取车辆列表时使用的输入数据传输对象，继承自 PagedAndSortedResultRequestDto，支持分页和排序。
/// 用于在获取车辆列表的操作中传递查询条件，如车辆状态、车辆识别号码等。
/// </summary>
public class GetListVehicleInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 可选的车辆状态查询条件，用于筛选特定状态的车辆。
    /// 如果该值为 null，则不根据状态进行筛选。
    /// </summary>
    public VehicleStatus? status { get; set; }

    /// <summary>
    /// 可选的车辆识别号码（VIN）查询条件，用于筛选特定 VIN 的车辆。
    /// 如果该值为 null，则不根据 VIN 进行筛选。
    /// </summary>
    public string? VIN { get; set; }
}
