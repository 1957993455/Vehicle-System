namespace VehicleApp.Domain.Shared.Enums;

/// <summary>
/// 支付状态
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// 未支付
    /// </summary>
    Unpaid = 0,

    /// <summary>
    /// 已支付
    /// </summary>
    Paid = 1,

    /// <summary>
    /// 已退款
    /// </summary>
    Refunded = 2
}