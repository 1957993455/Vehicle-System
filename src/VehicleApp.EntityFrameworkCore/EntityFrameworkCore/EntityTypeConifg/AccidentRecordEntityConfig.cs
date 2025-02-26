using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleApp.Domain.Vehicle;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore.EntityTypeConifg;

public class AccidentRecordEntityConfig : IEntityTypeConfiguration<AccidentRecordEntity>
{
    public void Configure(EntityTypeBuilder<AccidentRecordEntity> builder)
    {
        builder.ToTable("VehicleAccidentRecords", b => b.HasComment("车辆事故记录"));

        // 配置主键
        builder.HasKey(x => x.Id);

        // 配置基本属性
        builder.Property(x => x.AccidentDate)
            .IsRequired()
            .HasComment("事故发生日期");

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired()
            .HasComment("事故描述");

        builder.Property(x => x.RepairCost)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasComment("维修费用");

        // 配置外键关系
        builder.Property(x => x.VehicleId)
            .IsRequired()
            .HasComment("所属车辆ID");

        // 添加索引
        builder.HasIndex(x => x.VehicleId)
            .HasDatabaseName("IX_AccidentRecord_VehicleId");

        builder.HasIndex(x => x.AccidentDate)
            .HasDatabaseName("IX_AccidentRecord_Date");
        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}