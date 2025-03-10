using System;
using AutoMapper;
using VehicleApp.Application.Contracts.Order.Dtos;
using VehicleApp.Application.Contracts.Organization.Dtos;
using VehicleApp.Application.Contracts.Store.Dtos;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using VehicleApp.Domain.Order;
using VehicleApp.Domain.Store;
using VehicleApp.Domain.Vehicle;
using Volo.Abp.Identity;
using VehicleApp.Application.Contracts.AuditLogging;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.AuditLogging;

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
        CreateMap<StoreAggregateRoot, StoreDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Address.Province))
            .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src.Address.Detail))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.Address.District));
        CreateMap<CreateStoreInput, StoreAggregateRoot>();
        CreateMap<UpdateStoreInput, StoreAggregateRoot>();
        CreateMap<OrderAggregateRoot, OrderDto>();
        CreateMap<CreateOrderInput, OrderAggregateRoot>();
        CreateMap<UpdateOrderInput, OrderAggregateRoot>();
        CreateMap<AuditLog, AuditLogDto>();
        CreateMap<AuditLogAction, AuditLogActionDto>();
    }
}
