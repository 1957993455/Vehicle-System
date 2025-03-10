
using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace VehicleApp.Domain.ValueObjects;

/// <summary>
/// 地址值对象
/// </summary>
public class AddressValueObject : ValueObject
{


    private AddressValueObject()
    {
    }

    public AddressValueObject(string province, string city, string district, string street, string detail)
    {
        Province = province;
        City = city;
        District = district;
        Street = street;
        Detail = detail;
    }


    /// <summary>
    /// 省
    /// </summary>
    public string Province { get; set; }
    /// <summary>
    /// 市
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// 区
    /// </summary>
    public string District { get; set; }
    /// <summary>
    /// 街道
    /// </summary>
    public string Street { get; set; }
    /// <summary>
    /// 详细地址
    /// </summary>
    public string Detail { get; set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Province;
        yield return City;
        yield return District;
        yield return Street;
        yield return Detail;
    }
}
