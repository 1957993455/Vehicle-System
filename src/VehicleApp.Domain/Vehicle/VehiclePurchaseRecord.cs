using System;
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
        Guid id,
        Guid vehicleId,
        DateTime purchaseDate,
        decimal purchasePrice,
        string supplierName,
        string supplierPhoneNumber,
        string paymentMethod,
        string? remarks = null): base(id)
    {
        SetVehicleId(vehicleId);
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
