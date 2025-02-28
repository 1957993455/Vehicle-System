using System;
using AutoMapper;
using VehicleApp.Application.Contracts.Organization.Dtos;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using VehicleApp.Domain.Vehicle;
using Volo.Abp.Identity;

namespace VehicleApp.Application;

public class VehicleAppApplicationAutoMapperProfile : Profile
{
    public VehicleAppApplicationAutoMapperProfile()
    {
        CreateMap<VehicleAggregateRoot, VehicleDto>();
        CreateMap<MaintenanceRecordEntity, MaintenanceRecordDto>();
        CreateMap<OrganizationUnit, OrganizationUnitDto>().ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime));
        CreateMap<CreateOrganizationUnitInput, OrganizationUnit>();
        CreateMap<UpdateOrganizationUnitInput, OrganizationUnit>();
        CreateMap<VehiclePurchaseRecordEntity, VehiclePurchaseRecordDto>();
        CreateMap<CreateVehiclePurchaseRecordDto, VehiclePurchaseRecordEntity>();
        CreateMap<UpdateVehiclePurchaseRecordDto, VehiclePurchaseRecordEntity>();
    }
}
