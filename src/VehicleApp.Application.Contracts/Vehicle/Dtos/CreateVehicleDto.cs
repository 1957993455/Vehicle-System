using System;

namespace VehicleApp.Application.Contracts.Vehicle.Dtos;

/// <summary>
/// 创建车辆时使用的数据传输对象，包含了创建车辆所需的基本信息。
/// 用于在创建车辆的操作中传递必要的参数，不包含一些默认值或自动生成的信息。
/// </summary>
public class CreateVehicleDto
{
    /// <summary>
    /// 车辆识别号码（VIN），用于唯一标识车辆。
    /// </summary>
    public string VIN { get; set; }

    /// <summary>
    /// 车辆制造商，如丰田、宝马等。
    /// </summary>
    public string Make { get; set; }

    /// <summary>
    /// 车辆型号，如卡罗拉、X5 等。
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// 车辆生产年份。
    /// </summary>
    public string Year { get; set; }

    /// <summary>
    /// 车辆颜色。
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// 车辆行驶里程数。
    /// </summary>
    public int Mileage { get; set; }

    /// <summary>
    /// 车辆牌照号码。
    /// </summary>
    public string LicensePlate { get; set; }

    /// <summary>
    /// 车辆所属门店的唯一标识。
    /// </summary>
    public Guid StoreId { get; set; }

    /// <summary>
    /// 车辆所有者的唯一标识，所有者可以是用户或公司。
    /// </summary>
    public Guid OwnerId { get; set; }
}
