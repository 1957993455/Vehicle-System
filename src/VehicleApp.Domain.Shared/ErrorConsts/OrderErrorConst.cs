namespace VehicleApp.Domain.Shared.ErrorConsts;

public static class OrderErrorConst
{
    /// <summary>
    /// 订单不存在
    /// </summary>
    public const string OrderNotFound = "订单不存在";

    /// <summary>
    /// 订单已存在
    /// </summary>
    public const string OrderAlreadyExists = "订单已存在";

    /// <summary>
    /// 订单状态无效
    /// </summary>
    public const string OrderStatusInvalid = "订单状态无效";

    /// <summary>
    /// 订单金额无效
    /// </summary>
    public const string OrderAmountInvalid = "订单金额无效";

    /// <summary>
    /// 订单项不存在
    /// </summary>
    public const string OrderItemNotFound = "订单项不存在";

    /// <summary>
    /// 订单无法修改
    /// </summary>
    public const string OrderCannotBeModified = "订单无法修改";

    /// <summary>
    /// 订单无法取消
    /// </summary>
    public const string OrderCannotBeCancelled = "订单无法取消";

    /// <summary>
    /// 订单支付失败
    /// </summary>
    public const string OrderPaymentFailed = "订单支付失败";

    /// <summary>
    /// 订单配送失败
    /// </summary>
    public const string OrderDeliveryFailed = "订单配送失败";

    /// <summary>
    /// 订单用户不匹配
    /// </summary>
    public const string OrderUserNotMatch = "订单用户不匹配";

    /// <summary>
    /// 订单已过期
    /// </summary>
    public const string OrderExpired = "订单已过期";
}
