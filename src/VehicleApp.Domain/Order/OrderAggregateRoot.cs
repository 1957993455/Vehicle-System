using System;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace VehicleApp.Domain.Order;

/// <summary>
/// 订单聚合根
/// </summary>
public class OrderAggregateRoot : FullAuditedAggregateRoot<Guid>
{
    protected OrderAggregateRoot()
    {
    }

    /// <summary>
    /// 创建订单
    /// </summary>
    public OrderAggregateRoot(
        Guid vehicleId,
        Guid customerId,
        decimal totalAmount,
        DateTime? pickupTime,
        DateTime? returnTime,
        string remarks = null)
    {
        Status = OrderStatus.Created;
        PaymentStatus = PaymentStatus.Unpaid;

        SetVehicle(vehicleId);
        SetCustomer(customerId);
        SetAmount(totalAmount);
        SetSchedule(pickupTime, returnTime);
        SetRemarks(remarks);
    }

    public virtual void SetVehicle(Guid vehicleId)
    {
        if (vehicleId == Guid.Empty)
        {
            throw new ArgumentException("车辆ID不能为空", nameof(vehicleId));
        }
        VehicleId = vehicleId;
    }

    private void SetCustomer(Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            throw new ArgumentException("客户ID不能为空", nameof(customerId));
        }
        CustomerId = customerId;
    }

    private void SetAmount(decimal totalAmount)
    {
        if (totalAmount <= 0)
        {
            throw new ArgumentException("订单金额必须大于0", nameof(totalAmount));
        }
        TotalAmount = totalAmount;
    }

    private void SetSchedule(DateTime? pickupTime, DateTime? returnTime)
    {
        if (pickupTime.HasValue && returnTime.HasValue)
        {
            if (pickupTime.Value >= returnTime.Value)
            {
                throw new ArgumentException("还车时间必须晚于取车时间");
            }
            if (pickupTime.Value < DateTime.Now)
            {
                throw new ArgumentException("取车时间不能早于当前时间");
            }
        }
        PickupTime = pickupTime;
        ReturnTime = returnTime;
    }

    private void SetRemarks(string remarks)
    {
        Remarks = remarks?.Trim();
    }

    /// <summary>
    /// 车辆id
    /// </summary>
    public virtual Guid VehicleId { get; set; }

    /// <summary>
    /// 订单状态
    /// </summary>
    public virtual OrderStatus Status { get; set; }

    /// <summary>
    /// 客户id
    /// </summary>
    public virtual Guid CustomerId { get; set; }

    /// <summary>
    /// 订单金额
    /// </summary>
    public virtual decimal TotalAmount { get; set; }

    /// <summary>
    /// 支付方式
    /// </summary>
    public virtual PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// 支付状态
    /// </summary>
    public virtual PaymentStatus PaymentStatus { get; set; }

    /// <summary>
    /// 订单备注
    /// </summary>
    public virtual string Remarks { get; set; }

    /// <summary>
    /// 预约取车时间
    /// </summary>
    public virtual DateTime? PickupTime { get; set; }

    /// <summary>
    /// 预约还车时间
    /// </summary>
    public virtual DateTime? ReturnTime { get; set; }
}