
namespace VehicleApp.Domain.Shared.Enums;
/// <summary>
/// 支付方式
/// </summary>
public enum PaymentMethod
{
    /// <summary>
    /// 现金
    /// </summary>
    Cash = 0,

    /// <summary>
    /// 信用卡
    /// </summary>
    CreditCard = 1,

    /// <summary>
    /// 微信支付
    /// </summary>
    WeChatPay = 2,

    /// <summary>
    /// 支付宝
    /// </summary>
    AliPay = 3
}
