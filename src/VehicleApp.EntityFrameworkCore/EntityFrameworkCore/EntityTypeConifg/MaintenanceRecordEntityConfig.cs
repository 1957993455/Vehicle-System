using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleApp.Domain.Vehicle;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore.EntityTypeConifg;

public class MaintenanceRecordEntityConfig : IEntityTypeConfiguration<MaintenanceRecordEntity>
{
    public void Configure(EntityTypeBuilder<MaintenanceRecordEntity> builder)
    {
        builder.ToTable("VehicleMaintenanceRecords", b => b.HasComment("车辆维护记录"));

        // 配置主键
        builder.HasKey(x => x.Id);

        // 配置基本属性
        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired()
            .HasComment("维护项目描述");

        builder.Property(x => x.Cost)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasComment("维护费用");

        // 添加索引
        builder.HasIndex(x => x.VehicleId)
            .HasDatabaseName("IX_MaintenanceRecord_VehicleId");

        builder.HasIndex(x => x.CreationTime)
            .HasDatabaseName("IX_MaintenanceRecord_CreationTime");
        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}