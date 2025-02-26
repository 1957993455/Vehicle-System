using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleApp.Domain.Order;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore.EntityTypeConifg;

public class OrderDetailEntityConfig : IEntityTypeConfiguration<OrderDetailEntity>
{
    public void Configure(EntityTypeBuilder<OrderDetailEntity> builder)
    {
        builder.ToTable("OrderDetails", b => b.HasComment("订单明细"));

        // 配置主键
        builder.HasKey(x => x.Id);

        // 配置基本属性
        builder.Property(x => x.ItemName)
            .HasMaxLength(100)
            .IsRequired()
            .HasComment("项目名称");

        // 配置金额相关字段
        builder.Property(x => x.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasComment("单价");

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasComment("数量");

        builder.Property(x => x.SubTotal)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasComment("小计金额");

        builder.Property(x => x.DiscountAmount)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("折扣金额");

        builder.Property(x => x.ActualAmount)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasComment("实际金额");

        // 配置其他属性
        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .HasComment("描述");

        builder.Property(x => x.Sort)
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("排序");

        // 配置外键关系
        builder.Property(x => x.OrderId)
            .IsRequired()
            .HasComment("订单ID");

        // 配置导航属性
        builder.HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // 添加索引
        builder.HasIndex(x => x.OrderId)
            .HasDatabaseName("IX_OrderDetail_OrderId");

        builder.HasIndex(x => new { x.OrderId, x.Sort })
            .HasDatabaseName("IX_OrderDetail_OrderId_Sort");
        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}