using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Domain.Shared.Enums
{
    /// <summary>
    /// 门店状态枚举
    /// </summary>
    public enum StoreStatus
    {
        /// <summary>试运营</summary>
        Trial=1,

        /// <summary>正常营业</summary>
        Operational=2,

        /// <summary>装修中</summary>
        Renovating=4,

        /// <summary>暂停营业</summary>
        Suspended=8,

        /// <summary>永久关闭</summary>
        Closed=16
    }
}
