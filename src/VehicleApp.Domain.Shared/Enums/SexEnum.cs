using System.ComponentModel;

namespace VehicleApp.Domain.Shared.Enums
{
    public enum SexEnum
    {
        [Description("男")]
        Male = 1,

        [Description("女")]
        Female = 2,

        [Description("未知")]
        Unknown = 3
    }
}