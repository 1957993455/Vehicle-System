using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Vehicle;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using VehicleApp.Domain.Vehicle;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Application.Vehicle;

/// <summary>
/// 车辆购买记录应用服务实现
/// </summary>
public class VehiclePurchaseRecordAppService :
    CrudAppService<
        VehiclePurchaseRecordEntity,
        VehiclePurchaseRecordDto,
        Guid,
        GetVehiclePurchaseRecordListInput,
        CreateVehiclePurchaseRecordDto,
        UpdateVehiclePurchaseRecordDto>,
    IVehiclePurchaseRecordAppService
{
    protected IRepository<VehicleAggregateRoot, Guid> VehicleRepository { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">车辆购买记录仓储</param>
    /// <param name="vehicleRepository">车辆仓储</param>
    public VehiclePurchaseRecordAppService(IRepository<VehiclePurchaseRecordEntity, Guid> repository, IRepository<VehicleAggregateRoot, Guid> vehicleRepository)
        : base(repository)
    {
        VehicleRepository = vehicleRepository;
    }

    /// <summary>
    /// 创建查询过滤器
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>过滤后的查询对象</returns>
    protected override async Task<IQueryable<VehiclePurchaseRecordEntity>> CreateFilteredQueryAsync(GetVehiclePurchaseRecordListInput input)
    {
        var query = await base.CreateFilteredQueryAsync(input);

        // 根据VIN号筛选
        if (!string.IsNullOrWhiteSpace(input.Vin))
        {
            IQueryable<VehicleAggregateRoot> vehicleQuery = await VehicleRepository.GetQueryableAsync();
            var vehicleIds = vehicleQuery
                .Where(x => x.VIN.Equals(input.Vin))
                .Select(x => x.Id)
                .ToList();

            query = query.Where(x => vehicleIds.Contains(x.VehicleId));
        }

        // 根据购买日期范围筛选
        if (!string.IsNullOrWhiteSpace(input.StartDate) && !string.IsNullOrWhiteSpace(input.EndDate))
        {
            query = query.Where(x => x.PurchaseDate >= DateTime.Parse(input.StartDate) && x.PurchaseDate <= DateTime.Parse(input.EndDate));
        }

        return query;
    }

    /// <summary>
    /// 获取车辆购买记录列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>车辆购买记录列表</returns>
    public override async Task<PagedResultDto<VehiclePurchaseRecordDto>> GetListAsync(GetVehiclePurchaseRecordListInput input)
    {
        var result = await base.GetListAsync(input);

        if (!result.Items.Any())
        {
            return result;
        }

        var vehicleIds = result.Items.Select(x => x.VehicleId).Distinct().ToList();
        var vehicles = await VehicleRepository.GetListAsync(x => vehicleIds.Contains(x.Id));

        // 使用字典优化查找性能
        var vehicleDict = vehicles.ToDictionary(x => x.Id);

        foreach (var item in result.Items)
        {
            if (vehicleDict.TryGetValue(item.VehicleId, out var vehicle))
            {
                item.Brand = vehicle.Brand;
                item.Model = vehicle.Model;
                item.LicensePlateNumber = vehicle.LicensePlate;
                item.VIN = vehicle.VIN;
                item.ImageUrl = vehicle.ImageUrl ?? string.Empty;
                item.EngineType = vehicle.EngineType;
            }
        }

        return result;
    }
}
