using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace VehicleApp.Domain.Vehicle;

/// <summary>
/// 车辆管理
/// </summary>
public class VehicleManager : DomainService
{
    protected IVehicleRepository Repository { get; }

    public VehicleManager(IVehicleRepository repository) => Repository = repository;

    /// <summary>
    /// 删除车辆
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public virtual async Task DeleteAsync(Guid id)
    {
        Check.NotNull(id, nameof(id));

        var vehicle = await Repository.FindAsync(id);

        if (vehicle is null)
            throw new BusinessException($"车辆不存在");

        //清除车辆相关信息
        vehicle.MaintenanceRecords.Clear();
        vehicle.AccidentHistory.Clear();
        vehicle.Purchases.Clear();
        //删除车辆
        await Repository.DeleteAsync(vehicle);
    }
}
