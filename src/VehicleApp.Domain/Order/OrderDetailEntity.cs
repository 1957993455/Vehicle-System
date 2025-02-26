using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace VehicleApp.Domain.Order;

/// <summary>
/// 订单详情实体
/// </summary>
public class OrderDetailEntity : FullAuditedEntity<Guid>
{
    protected OrderDetailEntity()
    {
    }

    public OrderDetailEntity(
        Guid orderId,
        string itemName,
        decimal unitPrice,
        int quantity,
        string? description = null)
    {
        OrderId = orderId;
        ItemName = itemName;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Description = description ?? "";

        CalculateSubTotal();
    }

    /// <summary>
    /// 订单ID
    /// </summary>
    public Guid OrderId { get; protected set; }

    /// <summary>
    /// 项目名称（如：车辆租赁费、保险费、押金等）
    /// </summary>
    public string ItemName { get; protected set; }

    /// <summary>
    /// 单价
    /// </summary>
    public decimal UnitPrice { get; protected set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int Quantity { get; protected set; }

    /// <summary>
    /// 小计金额
    /// </summary>
    public decimal SubTotal { get; protected set; }

    /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; protected set; }

    /// <summary>
    /// 实际金额（小计 - 折扣）
    /// </summary>
    public decimal ActualAmount { get; protected set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; protected set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; protected set; }

    /// <summary>
    /// 导航属性 - 订单
    /// </summary>
    public virtual OrderAggregateRoot Order { get; protected set; }

    /// <summary>
    /// 计算小计金额
    /// </summary>
    private void CalculateSubTotal()
    {
        SubTotal = UnitPrice * Quantity;
        ActualAmount = SubTotal - DiscountAmount;
    }

    /// <summary>
    /// 设置折扣金额
    /// </summary>
    public void SetDiscount(decimal discountAmount)
    {
        if (discountAmount < 0)
        {
            throw new ArgumentException("折扣金额不能为负数");
        }
        if (discountAmount > SubTotal)
        {
            throw new ArgumentException("折扣金额不能大于小计金额");
        }

        DiscountAmount = discountAmount;
        CalculateSubTotal();
    }

    /// <summary>
    /// 更新数量
    /// </summary>
    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("数量必须大于0");
        }

        Quantity = quantity;
        CalculateSubTotal();
    }

    /// <summary>
    /// 更新单价
    /// </summary>
    public void UpdateUnitPrice(decimal unitPrice)
    {
        if (unitPrice <= 0)
        {
            throw new ArgumentException("单价必须大于0");
        }

        UnitPrice = unitPrice;
        CalculateSubTotal();
    }
}