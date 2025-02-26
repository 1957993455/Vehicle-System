using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Domain.Shared.Enums
{
    public enum EngineType
    {
        /// <summary>汽油发动机</summary>
        Gasoline,

        /// <summary>柴油发动机</summary>
        Diesel,

        /// <summary>纯电动驱动</summary>
        Electric,

        /// <summary>混合动力（燃油+电动）</summary>
        Hybrid
    }
}