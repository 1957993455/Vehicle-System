using System;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Store;
using VehicleApp.Application.Contracts.Store.Dtos;
using VehicleApp.Domain.Shared.Enums;
using VehicleApp.Domain.Store;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Application.Store;

public class StoreAppService :
    CrudAppService<
        StoreAggregateRoot,
        StoreDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateStoreDto,
        UpdateStoreDto>,
    IStoreAppService
{
    private readonly StoreManager _storeManager;

    public StoreAppService(
        IRepository<StoreAggregateRoot, Guid> repository,
        StoreManager storeManager)
        : base(repository)
    {
        _storeManager = storeManager;
    }

    public override async Task<StoreDto> CreateAsync(CreateStoreDto input)
    {
        var location = new GeoLocationValueObject(input.Longitude, input.Latitude);

        var store = await _storeManager.CreateAsync(
            input.Name,
            input.StoreCode,
            input.FullAddress,
            location,
            input.RegionId);

        await Repository.InsertAsync(store);
        return ObjectMapper.Map<StoreAggregateRoot, StoreDto>(store);
    }

    public async Task<StoreDto> ChangeStatusAsync(Guid id, StoreStatus newStatus)
    {
        var store = await Repository.GetAsync(id);
        store.ChangeStatus(newStatus);
        await Repository.UpdateAsync(store);
        return ObjectMapper.Map<StoreAggregateRoot, StoreDto>(store);
    }

    public async Task<StoreDto> RelocateAsync(Guid id, string newAddress, double longitude, double latitude)
    {
        var store = await Repository.GetAsync(id);
        store.Relocate(newAddress, longitude, latitude);
        await Repository.UpdateAsync(store);
        return ObjectMapper.Map<StoreAggregateRoot, StoreDto>(store);
    }
}