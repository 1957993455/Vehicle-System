using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Domain.Shared.Enums
{
    public enum FuelType
    {
        /// <summary>汽油燃料</summary>
        Gasoline,

        /// <summary>柴油燃料</summary>
        Diesel,

        /// <summary>电能（适用于电动车）</summary>
        Electric,

        /// <summary>氢能源（燃料电池车）</summary>
        Hydrogen
    }
}