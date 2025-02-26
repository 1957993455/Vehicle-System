using System;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Vehicle.Dtos;

public class MaintenanceRecordDto : AuditedEntityDto<Guid>
{
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public Guid VehicleId { get; set; }
}

public class CreateMaintenanceRecordDto
{
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public Guid VehicleId { get; set; }
}