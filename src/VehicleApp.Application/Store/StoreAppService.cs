using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Store;
using VehicleApp.Application.Contracts.Store.Dtos;
using VehicleApp.Domain.Shared;
using VehicleApp.Domain.Shared.Enums;
using VehicleApp.Domain.Store;
using VehicleApp.Domain.ValueObjects;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace VehicleApp.Application.Store;

/// <summary>
/// �ŵ�Ӧ�÷���
/// </summary>
public class StoreAppService :
    CrudAppService<
        StoreAggregateRoot,
        StoreDto,
        Guid,
        GetListStoreInput,
        CreateStoreInput,
        UpdateStoreInput>,
    IStoreAppService
{
    private readonly StoreManager _storeManager;
    protected IRepository<IdentityUser, Guid> UserRepository { get; }

    public StoreAppService(
        IRepository<StoreAggregateRoot, Guid> repository,
        StoreManager storeManager,
        IRepository<IdentityUser, Guid> userRepository)
        : base(repository)
    {
        _storeManager = storeManager;
        UserRepository = userRepository;
    }

    public override async Task<StoreDto> CreateAsync(CreateStoreInput input)
    {
        var location = new GeoLocationValueObject(input.Longitude, input.Latitude);
        var address = new AddressValueObject(input.Province, input.City, input.District, input.Street, input.Detail);
        var store = await _storeManager.CreateAsync(
            input.Name,
            input.StoreCode,
            address,
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

    protected override async Task<IQueryable<StoreAggregateRoot>> CreateFilteredQueryAsync(GetListStoreInput input)
    {
        var query = await base.CreateFilteredQueryAsync(input);

        return query.WhereIf(input.Status.HasValue, x => (input.Status != null && x.Status == input.Status))
           .WhereIf(!string.IsNullOrEmpty(input.Name), x => (input.Name != null && x.Name.Contains(input.Name)));
    }

    public override async Task<PagedResultDto<StoreDto>> GetListAsync(GetListStoreInput input)
    {
        PagedResultDto<StoreDto> result = await base.GetListAsync(input);
        //����id����ѯ��������Ϣ

        var managerIds = result.Items.Select(x => x.ManagerId).Distinct();

        var manangerNames = await UserRepository.GetListAsync(x => managerIds.Contains(x.Id));

        foreach (var item in result.Items)
        {
            var manager = manangerNames.FirstOrDefault(x => x.Id == item.ManagerId);

            if (manager == null)
            {
                item.ManagerName = "δ֪";
                item.ManagerPhone = "δ֪";
                continue;
            }

            item.ManagerName = manager.UserName;
            item.ManagerPhone = manager.PhoneNumber;
        }

        return result;
    }

    /// <summary>
    /// 批量删除门店
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task BatchDeleteAsync(List<Guid> ids)
    {
        var stores = await Repository.GetListAsync(x => ids.Contains(x.Id));
        await Repository.DeleteManyAsync(stores);
    }
}
