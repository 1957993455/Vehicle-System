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
        Trial,

        /// <summary>正常营业</summary>
        Operational,

        /// <summary>装修中</summary>
        Renovating,

        /// <summary>暂停营业</summary>
        Suspended,

        /// <summary>永久关闭</summary>
        Closed
    }
}