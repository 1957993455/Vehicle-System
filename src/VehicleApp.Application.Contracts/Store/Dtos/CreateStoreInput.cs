using System;

namespace VehicleApp.Application.Contracts.Store.Dtos;

public class CreateStoreInput
{
    public  string Name { get; set; }
    public string StoreCode { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public Guid RegionId { get; set; }
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
}
