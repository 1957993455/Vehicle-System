using System;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Vehicle.Dtos;

/// <summary>
/// 车辆数据传输对象，继承自 AuditedEntityDto<Guid>，用于在不同层之间传输车辆的完整信息。
/// 通常用于展示车辆的详细信息，包含了车辆的基本属性、状态以及关联的门店和所有者信息。
/// </summary>
public class VehicleDto : AuditedEntityDto<Guid>
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
    /// 车辆发动机类型，如柴油发动机、汽油发动机等。
    /// </summary>
    public EngineType EngineType { get; set; }

    /// <summary>
    /// 车辆变速箱类型，如自动变速箱、手动变速箱等。
    /// </summary>
    public TransmissionType TransmissionType { get; set; }

    /// <summary>
    /// 车辆燃料类型，如柴油、汽油、天然气等。
    /// </summary>
    public FuelType FuelType { get; set; }

    /// <summary>
    /// 车辆状态，如可用、维修中、已出租等。
    /// </summary>
    public VehicleStatus Status { get; set; }

    /// <summary>
    /// 车辆所属门店的唯一标识。
    /// </summary>
    public Guid StoreId { get; set; }

    /// <summary>
    /// 车辆所有者的唯一标识，所有者可以是用户或公司。
    /// </summary>
    public Guid OwnerId { get; set; }
}
