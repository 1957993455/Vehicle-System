using System;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace VehicleApp.Domain.Vehicle;

/// <summary>
/// 事故记录
/// </summary>
public class AccidentRecordEntity : FullAuditedEntity<Guid>
{
    protected AccidentRecordEntity()
    {
    }

    public AccidentRecordEntity(Guid id, DateTime accidentDate,
                                string description,
                                decimal repairCost,
                                Guid vehicleId,
                                string accidentLocation,
                                ClaimStatusEnum claimStatus,
                                Guid driverId,
                                string driverLicenseType,
                                DateTime reportDate,
                                string handlingDepartment,
                                string? insurancePolicyNumber = null,
                                string? insuranceCompany = null,
                                bool driverViolation = false) : base(id)
    {
        AccidentDate = accidentDate;
        Description = description;
        RepairCost = repairCost;
        VehicleId = vehicleId;
        AccidentLocation = accidentLocation;
        InsuranceCompany = insuranceCompany;
        InsurancePolicyNumber = insurancePolicyNumber;
        ClaimStatus = claimStatus;
        DriverId = driverId;
        DriverLicenseType = driverLicenseType;
        ReportDate = reportDate;
        HandlingDepartment = handlingDepartment;
        DriverViolation = driverViolation;
    }

    /// <summary>
    /// 日期
    /// </summary>
    public virtual DateTime AccidentDate { get; protected set; }

    /// <summary>
    /// 描述
    /// </summary>
    public virtual string Description { get; protected set; }

    /// <summary>
    /// 维修费用
    /// </summary>
    public virtual decimal RepairCost { get; protected set; }

    /// <summary>
    /// 车辆Id
    /// </summary>
    public virtual Guid VehicleId { get; protected set; }

    /// <summary>
    /// 事故发生地点
    /// </summary>
    public virtual string AccidentLocation { get; protected set; }

    /// <summary>
    /// 保险公司
    /// </summary>
    public virtual string? InsuranceCompany { get; protected set; }

    /// <summary>
    /// 保险单号
    /// </summary>
    public virtual string? InsurancePolicyNumber { get; protected set; }

    /// <summary>
    /// 理赔状态
    /// </summary>
    public virtual ClaimStatusEnum ClaimStatus { get; protected set; }

    /// <summary>
    /// 涉事驾驶员 ID
    /// </summary>
    public virtual Guid DriverId { get; protected set; }

    /// <summary>
    /// 驾驶员驾驶证类型
    /// </summary>
    public virtual string DriverLicenseType { get; protected set; }

    /// <summary>
    /// 事故报案日期
    /// </summary>
    public virtual DateTime ReportDate { get; protected set; }

    /// <summary>
    /// 事故处理部门
    /// </summary>
    public virtual string HandlingDepartment { get; protected set; }

    /// <summary>
    /// 驾驶员是否违规
    /// </summary>
    public virtual bool DriverViolation { get; protected set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public virtual DateTime? EndTime { get; protected set; }
}
