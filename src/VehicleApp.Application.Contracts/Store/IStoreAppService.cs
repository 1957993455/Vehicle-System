using System;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Store.Dtos;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VehicleApp.Application.Contracts.Store;

public interface IStoreAppService : ICrudAppService<
    StoreDto,
    Guid,
    GetListStoreInput,
    CreateStoreInput,
    UpdateStoreInput>
{
    Task<StoreDto> ChangeStatusAsync(Guid id, StoreStatus newStatus);

    Task<StoreDto> RelocateAsync(Guid id, string newAddress, double longitude, double latitude);
}
