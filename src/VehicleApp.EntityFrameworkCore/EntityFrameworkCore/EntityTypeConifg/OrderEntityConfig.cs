using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleApp.Domain.Order;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore.EntityTypeConifg;

public class OrderEntityConfig : IEntityTypeConfiguration<OrderAggregateRoot>
{
    public void Configure(EntityTypeBuilder<OrderAggregateRoot> builder)
    {
        builder.ToTable("Orders", b => b.HasComment("订单信息"));

        // 配置主键
        builder.HasKey(x => x.Id);

        // 配置关联关系
        builder.Property(x => x.VehicleId)
            .IsRequired()
            .HasComment("车辆ID");

        builder.Property(x => x.CustomerId)
            .IsRequired()
            .HasComment("客户ID");

        // 配置状态字段
        builder.Property(x => x.Status)
            .IsRequired()
            .HasComment("订单状态");

        builder.Property(x => x.PaymentStatus)
            .IsRequired()
            .HasComment("支付状态");

        builder.Property(x => x.PaymentMethod)
            .HasComment("支付方式");

        // 配置金额字段
        builder.Property(x => x.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasComment("订单总金额");

        // 配置时间字段
        builder.Property(x => x.PickupTime)
            .HasComment("预约取车时间");

        builder.Property(x => x.ReturnTime)
            .HasComment("预约还车时间");

        // 配置其他字段
        builder.Property(x => x.Remarks)
            .HasMaxLength(1000)
            .HasComment("订单备注");

        // 添加索引
        builder.HasIndex(x => x.VehicleId)
            .HasDatabaseName("IX_Order_VehicleId");

        builder.HasIndex(x => x.CustomerId)
            .HasDatabaseName("IX_Order_CustomerId");

        builder.HasIndex(x => x.Status)
            .HasDatabaseName("IX_Order_Status");

        builder.HasIndex(x => x.PaymentStatus)
            .HasDatabaseName("IX_Order_PaymentStatus");

        builder.HasIndex(x => x.PickupTime)
            .HasDatabaseName("IX_Order_PickupTime");

        // 复合索引
        builder.HasIndex(x => new { x.CustomerId, x.Status })
            .HasDatabaseName("IX_Order_CustomerId_Status");

        builder.HasIndex(x => new { x.VehicleId, x.Status })
            .HasDatabaseName("IX_Order_VehicleId_Status");

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}