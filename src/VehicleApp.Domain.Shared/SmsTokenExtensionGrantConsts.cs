namespace VehicleApp.Domain.Shared;

/// <summary>
/// 手机号验证扩展授权常量
/// </summary>
public static class SmsTokenExtensionGrantConsts
{
    /// <summary>
    /// 手机号验证
    /// </summary>
    public const string GrantType = "phone_verify";

    /// <summary>
    /// 手机号参数名
    /// </summary>
    public const string ParamName = "phone_number";

    /// <summary>
    /// 手机号验证码参数名
    /// </summary>
    public const string TokenName = "phone_verify_code";

    /// <summary>
    /// 手机号验证目的
    /// </summary>
    public const string Purpose = "phone_verify";

    /// <summary>
    /// 手机号验证码错误
    /// </summary>
    public const string SecurityCodeFailed = "SecurityCodeFailed";
}
