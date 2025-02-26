using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace VehicleApp.Domain.Store;

public class StoreManager : DomainService
{
    private readonly IRepository<StoreAggregateRoot, Guid> _storeRepository;

    public StoreManager(IRepository<StoreAggregateRoot, Guid> storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<StoreAggregateRoot> CreateAsync(
        string name,
        string storeCode,
        string fullAddress,
        GeoLocationValueObject location,
        Guid regionId)
    {
        // 检查编码唯一性
        if (await _storeRepository.AnyAsync(x => x.StoreCode == storeCode))
        {
            throw new BusinessException("Store:CodeAlreadyExists");
        }

        return new StoreAggregateRoot(
            name,
            storeCode,
            fullAddress,
            location,
            regionId);
    }
}