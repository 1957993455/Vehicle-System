using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Domain.Shared.Enums
{
    public enum TransmissionType
    {
        /// <summary>手动变速箱</summary>
        Manual,

        /// <summary>自动变速箱</summary>
        Automatic,

        /// <summary>半自动变速箱</summary>
        SemiAutomatic,

        /// <summary>无级变速器</summary>
        CVT
    }
}