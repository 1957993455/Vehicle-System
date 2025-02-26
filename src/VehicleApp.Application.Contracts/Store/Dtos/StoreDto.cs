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
}

public class CreateStoreDto
{
    public string Name { get; set; }
    public string StoreCode { get; set; }
    public string FullAddress { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public Guid RegionId { get; set; }
}

public class UpdateStoreDto
{
    public string Name { get; set; }
    public string BusinessHours { get; set; }
    public Guid? ManagerId { get; set; }
    public List<string> Tags { get; set; }
    public string Description { get; set; }
}