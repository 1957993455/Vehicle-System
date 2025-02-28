using System;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Vehicle.Dtos;

/// <summary>
/// 获取车辆购买记录列表的查询参数
/// </summary>
public class GetVehiclePurchaseRecordListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 搜索关键字，用于模糊查询品牌、型号、车牌号、VIN
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 按车辆ID筛选
    /// </summary>
    public Guid? VehicleId { get; set; }
}
