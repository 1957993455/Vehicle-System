using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    public string? Vin { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    public string? StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    public string? EndDate { get; set; }
}
