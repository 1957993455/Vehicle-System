using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Vehicle;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using VehicleApp.Domain.Shared.Enums;
using VehicleApp.Domain.Vehicle;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Application.Vehicle;

/// <summary>
/// 车辆应用服务
/// </summary>
/// <param name="repository"></param>
/// <param name="vehicleManager"></param>
public class VehicleAppService(IRepository<VehicleAggregateRoot, Guid> repository,VehicleManager vehicleManager) :
    CrudAppService<
        VehicleAggregateRoot,
        VehicleDto,
        Guid,
        GetListVehicleInput,
        CreateVehicleDto,
        UpdateVehicleDto>(repository),
    IVehicleAppService
{
    public async Task<VehicleDto> UpdateMileageAsync(Guid id, int newMileage)
    {
        var vehicle = await Repository.GetAsync(id);
        vehicle.UpdateMileage(newMileage);
        await Repository.UpdateAsync(vehicle);
        return ObjectMapper.Map<VehicleAggregateRoot, VehicleDto>(vehicle);
    }

    public async Task<VehicleDto> ChangeStatusAsync(Guid id, VehicleStatus newStatus)
    {
        var vehicle = await Repository.GetAsync(id);
        vehicle.ChangeStatus(newStatus);
        await Repository.UpdateAsync(vehicle);
        return ObjectMapper.Map<VehicleAggregateRoot, VehicleDto>(vehicle);
    }

    /// <summary>
    /// 构建查询过滤
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    protected override async Task<IQueryable<VehicleAggregateRoot>> CreateFilteredQueryAsync(GetListVehicleInput input)
    {
        IQueryable<VehicleAggregateRoot> query = await base.CreateFilteredQueryAsync(input);
        query = query.WhereIf(input.status != null, x => x.Status == input.status);
        query = query.WhereIf(input.VIN != null, x =>(input.VIN!= null && x.VIN.Contains(input.VIN)));
        return query;
    }

    /// <summary>
    /// 删除车辆
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task DeleteAsync(Guid id)
    {
        await CheckDeletePolicyAsync();

       await vehicleManager.DeleteAsync(id);
    }
}
