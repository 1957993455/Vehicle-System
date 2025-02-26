using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleApp.Domain.Store;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore.EntityTypeConifg;

public class StoreEntityConfig : IEntityTypeConfiguration<StoreAggregateRoot>
{
    public void Configure(EntityTypeBuilder<StoreAggregateRoot> builder)
    {
        builder.ToTable("Stores", b => b.HasComment("门店信息表"));

        // 配置主键
        builder.HasKey(x => x.Id);

        // 配置基础信息
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired()
            .HasComment("门店名称");

        builder.Property(x => x.StoreCode)
            .HasMaxLength(20)
            .IsRequired()
            .HasComment("门店编码");

        // 添加唯一索引
        builder.HasIndex(x => x.StoreCode)
            .IsUnique()
            .HasDatabaseName("UK_Store_Code");

        // 配置地理位置
        builder.Property(x => x.FullAddress)
            .HasMaxLength(200)
            .IsRequired()
            .HasComment("完整地址");

        // 配置地理位置值对象
        builder.OwnsOne(x => x.Location, location =>
        {
            location.Property(l => l.Longitude)
                .HasPrecision(9, 6)
                .IsRequired()
                .HasComment("经度");
            location.Property(l => l.Latitude)
                .HasPrecision(8, 6)
                .IsRequired()
                .HasComment("纬度");

            // 添加地理位置索引
            location.HasIndex(nameof(GeoLocationValueObject.Longitude), nameof(GeoLocationValueObject.Latitude))
                .HasDatabaseName("IX_Store_Location");
        });

        // 配置运营信息
        builder.Property(x => x.Status)
            .IsRequired()
            .HasComment("门店状态");

        // 配置关联信息
        builder.Property(x => x.RegionId)
            .IsRequired()
            .HasComment("所属区域ID");

        builder.Property(x => x.ManagerId)
            .HasComment("店长用户ID");

        // 添加区域索引
        builder.HasIndex(x => x.RegionId)
            .HasDatabaseName("IX_Store_RegionId");

        // 配置扩展属性
        builder.Property(x => x.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            )
            .HasMaxLength(1000)
            .HasComment("门店标签");

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .HasComment("门店描述");

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}