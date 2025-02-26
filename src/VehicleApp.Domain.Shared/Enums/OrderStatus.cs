namespace VehicleApp.Domain.Shared.Enums;
/// <summary>
/// 订单状态
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// 已创建
    /// </summary>
    Created = 0,

    /// <summary>
    /// 已确认
    /// </summary>
    Confirmed = 1,

    /// <summary>
    /// 已取车
    /// </summary>
    PickedUp = 2,

    /// <summary>
    /// 已还车
    /// </summary>
    Returned = 3,

    /// <summary>
    /// 已完成
    /// </summary>
    Completed = 4,

    /// <summary>
    /// 已取消
    /// </summary>
    Cancelled = 5
}
