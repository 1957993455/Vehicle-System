using System;
using VehicleApp.Domain.Shared.Enums;

namespace VehicleApp.Application.Contracts.Vehicle.Dtos;

/// <summary>
/// 创建车辆购买记录输入DTO
/// </summary>
public class CreateVehiclePurchaseRecordDto
{
    /// <summary>
    /// 关联车辆ID
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// 车辆品牌
    /// </summary>
    public string Brand { get; set; }

    /// <summary>
    /// 车辆型号
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// 车牌号
    /// </summary>
    public string LicensePlateNumber { get; set; }

    /// <summary>
    /// 车架号(VIN)
    /// </summary>
    public string VIN { get; set; }

    /// <summary>
    /// 发动机类型
    /// </summary>
    public EngineType EngineType { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime PurchaseDate { get; set; }

    /// <summary>
    /// 购买价格
    /// </summary>
    public decimal PurchasePrice { get; set; }

    /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

    /// <summary>
    /// 供应商联系电话
    /// </summary>
    public string SupplierPhoneNumber { get; set; }

    /// <summary>
    /// 支付方式
    /// </summary>
    public string PaymentMethod { get; set; }

    /// <summary>
    /// 备注信息
    /// </summary>
    public string Remarks { get; set; }
}
