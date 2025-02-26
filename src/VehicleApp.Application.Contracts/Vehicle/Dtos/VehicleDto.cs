using System;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Vehicle.Dtos;

public class VehicleDto : AuditedEntityDto<Guid>
{
    public string VIN { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Year { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }
    public string LicensePlate { get; set; }
    public EngineType EngineType { get; set; }
    public TransmissionType TransmissionType { get; set; }
    public FuelType FuelType { get; set; }
    public VehicleStatus Status { get; set; }
    public Guid StoreId { get; set; }
    public Guid OwnerId { get; set; }
}

public class CreateVehicleDto
{
    public string VIN { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Year { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }
    public string LicensePlate { get; set; }
    public Guid StoreId { get; set; }
    public Guid OwnerId { get; set; }
}

public class UpdateVehicleDto
{
    public int Mileage { get; set; }
    public VehicleStatus Status { get; set; }
}

public class GetListVehicleInput : PagedAndSortedResultRequestDto
{
    public VehicleStatus? status { get; set; }

    public string? VIN { get; set; }


}