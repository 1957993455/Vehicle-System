using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace VehicleApp.Domain.Vehicle;

/// <summary>
/// 车辆维护记录实体（归属于车辆聚合根的明细记录）
/// </summary>

public class MaintenanceRecordEntity : FullAuditedEntity<Guid>
{
    /// <summary>
    /// 维护项目描述
    /// </summary>
    /// <example>更换机油和滤清器 | 轮胎四轮定位 | 电池系统检测</example>
    public virtual string Description { get; set; }

    /// <summary>
    /// 维护费用
    /// </summary>
    public virtual decimal Cost { get; set; }

    /// <summary>
    /// 关联的车辆聚合根ID（外键）
    public virtual Guid VehicleId { get; set; }

    public virtual VehicleAggregateRoot Vehicle { get; set; }
}