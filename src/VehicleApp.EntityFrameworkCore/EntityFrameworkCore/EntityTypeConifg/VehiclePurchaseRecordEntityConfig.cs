using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleApp.Domain.Vehicle;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore.EntityTypeConifg;

/// <summary>
/// 车辆购买记录实体配置类
/// </summary>
public class VehiclePurchaseRecordEntityConfig : IEntityTypeConfiguration<VehiclePurchaseRecordEntity>
{
    /// <summary>
    /// 配置实体映射
    /// </summary>
    /// <param name="builder">实体类型构建器</param>
    public void Configure(EntityTypeBuilder<VehiclePurchaseRecordEntity> builder)
    {
        builder.ToTable("VehiclePurchaseRecords", b => b.HasComment("车辆购买记录"));

        // 配置主键
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PurchaseDate)
            .IsRequired()
            .HasComment("购买日期");

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasComment("购买价格");

        builder.Property(x => x.SupplierName)
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("供应商名称");

        builder.Property(x => x.SupplierPhoneNumber)
            .IsRequired()
            .HasMaxLength(11)
            .HasComment("供应商电话");

        builder.Property(x => x.PaymentMethod)
            .IsRequired()
            .HasMaxLength(50)
            .HasComment("支付方式");

        builder.Property(x => x.Remarks)
            .HasMaxLength(500)
            .HasComment("备注");

        // 添加索引
        builder.HasIndex(x => x.VehicleId)
            .HasDatabaseName("IX_VehiclePurchaseRecord_VehicleId");

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
