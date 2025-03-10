using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleApp.Domain.Vehicle;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore.EntityTypeConifg;

public class VehicleEntityConfig : IEntityTypeConfiguration<VehicleAggregateRoot>
{
    public void Configure(EntityTypeBuilder<VehicleAggregateRoot> builder)
    {
        builder.ToTable("Vehicle");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasConversion<Guid>().IsRequired();
        builder.Property(v => v.VIN).HasMaxLength(50).IsRequired();
        builder.Property(v => v.Brand).HasMaxLength(100).IsRequired();
        builder.Property(v => v.Model).HasMaxLength(100).IsRequired();
        builder.Property(v => v.Year).HasMaxLength(4).IsRequired();
        builder.Property(v => v.Color).HasMaxLength(50).IsRequired();
        builder.Property(v => v.Mileage).IsRequired();
        builder.Property(v => v.LicensePlate).HasMaxLength(20).IsRequired();
        builder.Property(v => v.EngineType).IsRequired();
        builder.Property(v => v.TransmissionType).IsRequired();
        builder.Property(v => v.FuelType).IsRequired();
        builder.Property(v => v.Status).IsRequired();
        builder.Property(v => v.StoreId).IsRequired();
        builder.Property(v => v.OwnerId).IsRequired();
        builder.Property(v => v.ImageUrl).IsRequired(false);
        builder.OwnsOne(x => x.Address, y =>
        {
            y.Property(a=>a.City).HasMaxLength(100).IsRequired();
            y.Property(a=>a.Province).HasMaxLength(100).IsRequired();
            y.Property(a => a.Street).HasMaxLength(100).IsRequired();
            y.Property(a => a.Detail).HasMaxLength(100).IsRequired();
            y.Property(a => a.District).HasMaxLength(100).IsRequired();
        });
        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
