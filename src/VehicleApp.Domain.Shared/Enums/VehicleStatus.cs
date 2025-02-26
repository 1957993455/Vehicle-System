using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Domain.Shared.Enums;

public enum VehicleStatus
{
    /// <summary>
    /// 车辆可用
    /// </summary>
    Available,

    /// <summary>
    /// 车辆在维修
    /// </summary>
    InMaintenance,

    /// <summary>
    /// 车辆已售出
    /// </summary>
    Sold
}