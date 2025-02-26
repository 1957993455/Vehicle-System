using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Values;

namespace VehicleApp.Domain.Store
{
    /// <summary>
    /// 地理坐标值对象
    /// </summary>
    public class GeoLocationValueObject : ValueObject
    {
        public GeoLocationValueObject(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>经度（-180到180）</summary>
        [Range(-180, 180)]
        public double Longitude { get; private set; }

        /// <summary>纬度（-90到90）</summary>
        [Range(-90, 90)]
        public double Latitude { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Longitude;
            yield return Latitude;
        }
    }
}
