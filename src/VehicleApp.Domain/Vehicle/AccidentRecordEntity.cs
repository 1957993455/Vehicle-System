using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace VehicleApp.Domain.Vehicle;

/// <summary>
/// 事故记录
/// </summary>
public class AccidentRecordEntity : FullAuditedEntity<Guid>
{
    public DateTime AccidentDate { get; set; }
    public string Description { get; set; }
    public decimal RepairCost { get; set; }
    public Guid VehicleId { get; set; }
}