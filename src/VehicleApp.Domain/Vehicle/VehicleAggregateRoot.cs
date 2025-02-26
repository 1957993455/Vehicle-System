using System;
using System.Collections.Generic;
using System.Drawing;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace VehicleApp.Domain.Vehicle;

/// <summary>
/// 车辆聚合
/// </summary>
public class VehicleAggregateRoot : FullAuditedAggregateRoot<Guid>
{
    protected VehicleAggregateRoot()
    { }

    public VehicleAggregateRoot(Guid id, string vin, string make, string model, string year, string color, int mileage, string licensePlateGuid, Guid storeId, Guid ownerId)
    {
        // 验证参数合法性
        Check.NotNullOrWhiteSpace(vin, nameof(vin));
        Check.NotNullOrWhiteSpace(make, nameof(make));
        Check.NotNullOrWhiteSpace(model, nameof(model));
        Check.NotNullOrWhiteSpace(year, nameof(year));
        Check.NotNullOrWhiteSpace(color, nameof(color));
        Check.NotNullOrWhiteSpace(licensePlateGuid, nameof(licensePlateGuid));
        Check.NotNull(storeId, nameof(storeId));
        Check.NotNull(ownerId, nameof(ownerId));

        // 初始化基本属性
        Id = id;
        VIN = vin;
        Make = make;
        Model = model;
        Year = year;
        Color = color;
        Mileage = mileage;
        LicensePlate = licensePlateGuid;
        EngineType = EngineType.Diesel;
        TransmissionType = TransmissionType.Automatic;
        FuelType = FuelType.Diesel;
        Status = VehicleStatus.Available;
        StoreId = storeId;
        OwnerId = ownerId;
        MaintenanceRecords = new List<MaintenanceRecordEntity>();
        AccidentHistory = new List<AccidentRecordEntity>();
    }

    // 基本属性
    public virtual string VIN { get; set; }

    public virtual string Make { get; set; }
    public virtual string Model { get; set; }
    public virtual string Year { get; set; }
    public virtual string Color { get; set; }
    public virtual int Mileage { get; set; }
    public virtual string LicensePlate { get; set; }
    public virtual EngineType EngineType { get; set; }
    public virtual TransmissionType TransmissionType { get; set; }
    public virtual FuelType FuelType { get; set; }
    public virtual VehicleStatus Status { get; set; }

    /// <summary>
    /// 门店Id
    /// </summary>
    public virtual Guid StoreId { get; set; }

    // 关联实体
    public virtual Guid OwnerId { get; set; } // 假设所有者是一个用户或公司

    public virtual ICollection<MaintenanceRecordEntity> MaintenanceRecords { get; protected set; }
    public virtual ICollection<AccidentRecordEntity> AccidentHistory { get; protected set; }

    // 方法
    public void UpdateMileage(int newMileage)
    {
        if (newMileage < 0)
            throw new ArgumentException("Mileage cannot be negative.");

        Mileage = newMileage;
    }

    public void ChangeStatus(VehicleStatus newStatus)
    {
        Status = newStatus;
    }

    public void AddMaintenanceRecord(MaintenanceRecordEntity record)
    {
        MaintenanceRecords.Add(record);
    }

    public void AddAccidentRecord(AccidentRecordEntity record)
    {
        AccidentHistory.Add(record);
    }
}
