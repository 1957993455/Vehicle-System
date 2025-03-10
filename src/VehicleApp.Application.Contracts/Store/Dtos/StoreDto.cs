using System;
using System.Collections.Generic;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Store.Dtos;

public class StoreDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }
    public string StoreCode { get; set; }
    public string FullAddress { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string BusinessHours { get; set; }
    public StoreStatus Status { get; set; }
    public Guid RegionId { get; set; }
    public Guid? ManagerId { get; set; }
    public List<string> Tags { get; set; }
    public string Description { get; set; }
    public string ManagerName { get; set; }
    public string ManagerPhone { get; set; }
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
