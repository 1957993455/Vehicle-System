using System;
using System.Collections.Generic;
using System.Drawing;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace VehicleApp.Domain.Vehicle;

/// <summary>
/// 车辆聚合根类，继承自 FullAuditedAggregateRoot，使用 Guid 作为主键类型
/// 该类代表车辆的核心业务对象，包含车辆的基本信息、关联实体以及相关操作方法
/// </summary>
public class VehicleAggregateRoot : FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 受保护的无参构造函数，供 ORM 框架使用
    /// </summary>
    protected VehicleAggregateRoot()
    { }

    /// <summary>
    /// 有参构造函数，用于初始化车辆对象
    /// </summary>
    /// <param name="id">车辆的唯一标识</param>
    /// <param name="vin">车辆识别号码（VIN），用于唯一标识车辆</param>
    /// <param name="make">车辆制造商，如丰田、宝马等</param>
    /// <param name="model">车辆型号，如卡罗拉、X5 等</param>
    /// <param name="year">车辆生产年份</param>
    /// <param name="color">车辆颜色</param>
    /// <param name="mileage">车辆行驶里程数</param>
    /// <param name="licensePlateGuid">车辆牌照号码</param>
    /// <param name="storeId">车辆所属门店的唯一标识</param>
    /// <param name="ownerId">车辆所有者的唯一标识，所有者可以是用户或公司</param>
    public VehicleAggregateRoot(
        Guid id,
        string vin,
        string brand,
        string model,
        string year,
        string color,
        int mileage,
        string licensePlateGuid,
        Guid storeId,
        Guid ownerId)
    {
        // 验证参数合法性，确保传入的参数不为空或仅包含空白字符
        Check.NotNullOrWhiteSpace(vin, nameof(vin));
        Check.NotNullOrWhiteSpace(brand, nameof(brand));
        Check.NotNullOrWhiteSpace(model, nameof(model));
        Check.NotNullOrWhiteSpace(year, nameof(year));
        Check.NotNullOrWhiteSpace(color, nameof(color));
        Check.NotNullOrWhiteSpace(licensePlateGuid, nameof(licensePlateGuid));
        Check.NotNull(storeId, nameof(storeId));
        Check.NotNull(ownerId, nameof(ownerId));

        // 初始化基本属性
        Id = id;
        VIN = vin;
        Brand = brand;
        Model = model;
        Year = year;
        Color = color;
        Mileage = mileage;
        LicensePlate = licensePlateGuid;
        // 初始发动机类型设为柴油发动机
        EngineType = EngineType.Diesel;
        // 初始变速箱类型设为自动变速箱
        TransmissionType = TransmissionType.Automatic;
        // 初始燃料类型设为柴油
        FuelType = FuelType.Diesel;
        // 初始车辆状态设为可用
        Status = VehicleStatus.Available;
        StoreId = storeId;
        OwnerId = ownerId;
        // 初始化维护记录集合
        MaintenanceRecords = new List<MaintenanceRecordEntity>();
        // 初始化事故历史记录集合
        AccidentHistory = new List<AccidentRecordEntity>();
        // 初始化购买记录集合
        Purchases = new List<VehiclePurchaseRecordEntity>();
    }

    /// <summary>
    /// 车辆识别号码（VIN），用于唯一标识车辆
    /// </summary>
    public virtual string VIN { get; protected set; }


    /// <summary>
    /// 车辆图片Url
    /// </summary>
    public virtual string? ImageUrl { get; set; }

    /// <summary>
    /// 车辆制造商，如丰田、宝马等
    /// </summary>
    public virtual string Brand { get; protected set; }

    /// <summary>
    /// 车辆型号，如卡罗拉、X5 等
    /// </summary>
    public virtual string Model { get; protected set; }

    /// <summary>
    /// 车辆生产年份
    /// </summary>
    public virtual string Year { get; protected set; }

    /// <summary>
    /// 车辆颜色
    /// </summary>
    public virtual string Color { get; protected set; }

    /// <summary>
    /// 车辆行驶里程数
    /// </summary>
    public virtual int Mileage { get; protected set; }

    /// <summary>
    /// 车辆牌照号码
    /// </summary>
    public virtual string LicensePlate { get; protected set; }

    /// <summary>
    /// 车辆发动机类型，如柴油发动机、汽油发动机等
    /// </summary>
    public virtual EngineType EngineType { get; protected set; }

    /// <summary>
    /// 车辆变速箱类型，如自动变速箱、手动变速箱等
    /// </summary>
    public virtual TransmissionType TransmissionType { get; protected set; }

    /// <summary>
    /// 车辆燃料类型，如柴油、汽油、天然气等
    /// </summary>
    public virtual FuelType FuelType { get; protected set; }

    /// <summary>
    /// 车辆状态，如可用、维修中、已出租等
    /// </summary>
    public virtual VehicleStatus Status { get; protected set; }

    /// <summary>
    /// 门店Id，车辆所属门店的唯一标识
    /// </summary>
    public virtual Guid StoreId { get; protected set; }

    /// <summary>
    /// 车辆所有者的唯一标识，所有者可以是用户或公司
    /// </summary>
    public virtual Guid OwnerId { get; protected set; }

    /// <summary>
    /// 车辆的维护记录集合，记录车辆的维护历史信息
    /// </summary>
    public virtual ICollection<MaintenanceRecordEntity> MaintenanceRecords { get; protected set; }

    /// <summary>
    /// 车辆的事故历史记录集合，记录车辆的事故历史信息
    /// </summary>
    public virtual ICollection<AccidentRecordEntity> AccidentHistory { get; protected set; }

    /// <summary>
    /// 车辆的购买记录集合，记录车辆的购买历史信息
    /// </summary>
    public virtual ICollection<VehiclePurchaseRecordEntity> Purchases { get; protected set; }

    /// <summary>
    /// 更新车辆行驶里程数
    /// </summary>
    /// <param name="newMileage">新的行驶里程数</param>
    /// <exception cref="ArgumentException">当新的行驶里程数为负数时抛出异常</exception>
    public virtual void UpdateMileage(int newMileage)
    {
        if (newMileage < 0)
            throw new ArgumentException("Mileage cannot be negative.");

        Mileage = newMileage;
    }

    /// <summary>
    /// 更改车辆状态
    /// </summary>
    /// <param name="newStatus">新的车辆状态</param>
    public virtual void ChangeStatus(VehicleStatus newStatus)
    {
        Status = newStatus;
    }

    /// <summary>
    /// 添加车辆维护记录
    /// </summary>
    /// <param name="record">要添加的维护记录实体</param>
    public virtual void AddMaintenanceRecord(MaintenanceRecordEntity record)
    {
        MaintenanceRecords.Add(record);
    }

    /// <summary>
    /// 添加车辆事故记录
    /// </summary>
    /// <param name="record">要添加的事故记录实体</param>
    public virtual void AddAccidentRecord(AccidentRecordEntity record)
    {
        AccidentHistory.Add(record);
    }
}
