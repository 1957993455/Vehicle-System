﻿using System;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using System.Text.RegularExpressions;

namespace VehicleApp.Domain.Vehicle;

/// <summary>
/// 车辆购买记录实体类，用于存储车辆购买的相关信息
/// </summary>
public class VehiclePurchaseRecordEntity : FullAuditedEntity<Guid>
{
    protected VehiclePurchaseRecordEntity()
    {
    }

    public VehiclePurchaseRecordEntity(
        Guid vehicleId,
        string brand,
        string model,
        string licensePlateNumber,
        string vin,
        EngineType engineType,
        DateTime purchaseDate,
        decimal purchasePrice,
        string supplierName,
        string supplierPhoneNumber,
        string paymentMethod,
        string? remarks = null)
    {
        SetVehicleId(vehicleId);
        SetBrand(brand);
        SetModel(model);
        SetLicensePlate(licensePlateNumber);
        SetVIN(vin);
        SetEngineType(engineType);
        SetPurchaseInfo(purchaseDate, purchasePrice);
        SetSupplierInfo(supplierName, supplierPhoneNumber);
        SetPaymentMethod(paymentMethod);
        SetRemarks(remarks);
    }

    /// <summary>
    /// 车辆的唯一标识符，关联车辆信息表
    /// </summary>
    public virtual Guid VehicleId { get; protected set; }

    /// <summary>
    /// 车辆的品牌，例如丰田、大众等
    /// </summary>
    public virtual string Brand { get; protected set; } = null!;

    /// <summary>
    /// 车辆的具体型号，如卡罗拉、帕萨特等
    /// </summary>
    public virtual string Model { get; protected set; } = null!;

    /// <summary>
    /// 车辆的车牌号，用于唯一标识在路上行驶的车辆
    /// </summary>
    public virtual string LicensePlateNumber { get; protected set; } = null!;

    /// <summary>
    /// 车辆的车架号，是车辆的唯一识别代码
    /// </summary>
    public virtual string VIN { get; protected set; } = null!;

    /// <summary>
    /// 车辆的发动机类型
    /// </summary>
    public virtual EngineType EngineType { get; protected set; }

    /// <summary>
    /// 车辆的购买日期，记录车辆购买的具体时间
    /// </summary>
    public virtual DateTime PurchaseDate { get; protected set; }

    /// <summary>
    /// 车辆的购买价格，以货币形式表示
    /// </summary>
    public virtual decimal PurchasePrice { get; protected set; }

    /// <summary>
    /// 车辆的供应商名称，即车辆的销售方
    /// </summary>
    public virtual string SupplierName { get; protected set; } = null!;

    /// <summary>
    /// 供应商的联系电话，方便后续沟通和查询
    /// </summary>
    public virtual string SupplierPhoneNumber { get; protected set; } = null!;

    /// <summary>
    /// 购买车辆时的付款方式，如现金、转账、贷款等
    /// </summary>
    public virtual string PaymentMethod { get; protected set; } = null!;

    /// <summary>
    /// 购买车辆的备注信息，可用于记录一些特殊情况或说明
    /// </summary>
    public virtual string Remarks { get; protected set; } = null!;



    private void SetVehicleId(Guid vehicleId)
    {
        Check.NotNull(vehicleId, nameof(vehicleId));
        VehicleId = vehicleId;
    }

    private void SetBrand(string brand)
    {
        Brand = Check.NotNullOrWhiteSpace(
            brand,
            nameof(brand),
            maxLength: 50);
    }

    private void SetModel(string model)
    {
        Model = Check.NotNullOrWhiteSpace(
            model,
            nameof(model),
            maxLength: 50);
    }

    private void SetLicensePlate(string licensePlate)
    {
        if (!Regex.IsMatch(licensePlate, @"^[\u4e00-\u9fa5][A-Z][A-Z0-9]{5}$"))
        {
            throw new BusinessException("Vehicle:InvalidLicensePlate");
        }
        LicensePlateNumber = licensePlate;
    }

    private void SetVIN(string vin)
    {
        if (!Regex.IsMatch(vin, @"^[A-HJ-NPR-Z0-9]{17}$"))
        {
            throw new BusinessException("Vehicle:InvalidVIN");
        }
        VIN = vin;
    }

    private void SetEngineType(EngineType engineType)
    {
        if (!Enum.IsDefined(typeof(EngineType), engineType))
        {
            throw new BusinessException("Vehicle:InvalidEngineType");
        }
        EngineType = engineType;
    }

    private void SetPurchaseInfo(DateTime purchaseDate, decimal purchasePrice)
    {
        if (purchaseDate > DateTime.Now)
        {
            throw new BusinessException("Vehicle:FuturePurchaseDate");
        }
        if (purchasePrice <= 0)
        {
            throw new BusinessException("Vehicle:InvalidPurchasePrice");
        }

        PurchaseDate = purchaseDate;
        PurchasePrice = purchasePrice;
    }

    private void SetSupplierInfo(string supplierName, string supplierPhone)
    {
        SupplierName = Check.NotNullOrWhiteSpace(
            supplierName,
            nameof(supplierName),
            maxLength: 100);

        if (!Regex.IsMatch(supplierPhone, @"^1[3-9]\d{9}$"))
        {
            throw new BusinessException("Vehicle:InvalidSupplierPhone");
        }
        SupplierPhoneNumber = supplierPhone;
    }

    private void SetPaymentMethod(string paymentMethod)
    {
        PaymentMethod = Check.NotNullOrWhiteSpace(
            paymentMethod,
            nameof(paymentMethod),
            maxLength: 50);
    }

    private void SetRemarks(string? remarks)
    {
        Remarks = remarks?.Trim() ?? string.Empty;
    }

}
