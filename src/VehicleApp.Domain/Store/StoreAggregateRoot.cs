using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using VehicleApp.Domain.Shared.Enums;
using VehicleApp.Domain.ValueObjects;

namespace VehicleApp.Domain.Store
{
    /// <summary>
    /// 门店聚合根（表示实体门店业务单元，包含完整的门店信息和业务规则）
    /// </summary>
    public class StoreAggregateRoot : FullAuditedAggregateRoot<Guid>
    {
        #region 基础信息

        /// <summary>
        /// 门店名称（显示名称）
        /// </summary>
        /// <example>XX品牌朝阳旗舰店 | 王府井分店</example>
        /// <remarks>
        /// 长度限制：2-100字符
        /// 格式要求：需包含品牌+位置标识
        /// </remarks>
        public string Name { get; protected init; }

        /// <summary>
        /// 门店唯一编码（业务标识）
        /// </summary>
        /// <example>STORE_BJ_CY_001 | STORE_SH_HP_002</example>
        /// <remarks>
        /// 格式规则：STORE_区域代码_位置简写_序号
        /// 唯一性约束：品牌内唯一
        /// </remarks>
        public string StoreCode { get; private set; }

        /// <summary>
        /// 地址值对象
        /// </summary>
        public AddressValueObject Address { get; private set; }

        #endregion 基础信息

        /// <summary>
        /// 地理坐标值对象
        /// </summary>
        public GeoLocationValueObject Location { get; private set; }


        #region 运营信息

        /// <summary>
        /// 营业时间配置
        /// </summary>
        public string BusinessHours { get; protected set; }

        /// <summary>
        /// 门店状态
        /// </summary>
        public StoreStatus Status { get; protected set; }

        #endregion 运营信息

        #region 关联信息

        /// <summary>
        /// 所属区域ID（关联区域聚合根）
        /// </summary>
        public Guid RegionId { get; private set; }

        /// <summary>
        /// 店长用户ID（关联用户聚合根）
        /// </summary>
        public Guid? ManagerId { get; private set; }

        #endregion 关联信息

        #region 扩展属性

        /// <summary>
        /// 门店特色标签
        /// </summary>
        /// <example>["24小时营业", "宠物友好", "无障碍设施"]</example>
        public ICollection<string> Tags { get; protected set; } = new List<string>();

        /// <summary>
        /// 门店描述（富文本）
        /// </summary>
        /// <remarks>
        /// 支持HTML内容（需过滤XSS）
        /// 最大长度：2000字符
        /// </remarks>
        public string? Description { get; protected set; }

        #endregion 扩展属性

        #region 领域方法

        /// <summary>
        /// 更新营业状态
        /// </summary>
        /// <param name="newStatus">目标状态</param>
        /// <exception cref="BusinessException">
        /// 当试图从Closed状态恢复时抛出
        /// </exception>
        public void ChangeStatus(StoreStatus newStatus)
        {
            if (Status == StoreStatus.Closed && newStatus != StoreStatus.Closed)
                throw new BusinessException("ERR-STORE-001", "已关闭门店不可重新激活");

            Status = newStatus;
        }

        /// <summary>
        /// 设置营业时间
        /// </summary>
        /// <param name="businessHours"></param>
        public virtual void SetBusinessHours(string businessHours)
        {
            BusinessHours = businessHours;
        }

        /// <summary>
        /// 更新地理位置
        /// </summary>
        /// <param name="newAddress">完整地址</param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        public void Relocate(AddressValueObject newAddress, double longitude, double latitude)
        {
            if (Status == StoreStatus.Operational)
                throw new BusinessException("ERR-STORE-002", "营业中门店不可变更位置");

            Location = new GeoLocationValueObject(longitude, latitude);
            Address = newAddress;
        }

        #endregion 领域方法

        #region 构造函数

        protected StoreAggregateRoot()
        { } // EF Core需要

        public StoreAggregateRoot(
            string name,
            string storeCode,
            AddressValueObject address,
            GeoLocationValueObject location,
            Guid regionId)
        {
            ValidateName(name);
            ValidateStoreCode(storeCode);

            Name = name;
            StoreCode = storeCode.ToUpperInvariant();
            Address = address;
            Location = location;
            RegionId = regionId;
            Status = StoreStatus.Trial;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length is < 2 or > 100)
                throw new BusinessException("ERR-STORE-003", "门店名称长度需为2-100字符");
        }

        private void ValidateStoreCode(string code)
        {
            var regex = new Regex(@"^STORE_[A-Z]{2}_[A-Z]{2}_\d{3}$");
            if (!regex.IsMatch(code))
                throw new BusinessException("ERR-STORE-004", "门店编码格式无效");
        }

        #endregion 构造函数
    }
}
