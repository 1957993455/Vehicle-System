using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Vehicle;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using VehicleApp.Domain.Vehicle;
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
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">车辆购买记录仓储</param>
    public VehiclePurchaseRecordAppService(IRepository<VehiclePurchaseRecordEntity, Guid> repository)
        : base(repository)
    {
    }

    /// <summary>
    /// 创建查询过滤器
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>过滤后的查询对象</returns>
    protected override async Task<IQueryable<VehiclePurchaseRecordEntity>> CreateFilteredQueryAsync(GetVehiclePurchaseRecordListInput input)
    {
        var query = await base.CreateFilteredQueryAsync(input);

        return query
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x =>
                x.Brand.Contains(input.Filter) ||
                x.Model.Contains(input.Filter) ||
                x.LicensePlateNumber.Contains(input.Filter) ||
                x.VIN.Contains(input.Filter))
            .WhereIf(input.VehicleId.HasValue, x => x.VehicleId == input.VehicleId);
    }
}
