using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.Vehicle;
using VehicleApp.Application.Contracts.Vehicle.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.HttpApi.Controllers;

/// <summary>
/// 车辆购买记录API控制器
/// </summary>
[RemoteService]
[Route("api/vehicle-purchase-records")]
[Authorize]
public class VehiclePurchaseRecordController : VehicleAppController
{
    private readonly IVehiclePurchaseRecordAppService _purchaseRecordAppService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="purchaseRecordAppService">车辆购买记录应用服务</param>
    public VehiclePurchaseRecordController(IVehiclePurchaseRecordAppService purchaseRecordAppService)
    {
        _purchaseRecordAppService = purchaseRecordAppService;
    }

    /// <summary>
    /// 获取车辆购买记录列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页后的车辆购买记录列表</returns>
    [HttpGet]
    public Task<PagedResultDto<VehiclePurchaseRecordDto>> GetListAsync([FromQuery] GetVehiclePurchaseRecordListInput input)
    {
        return _purchaseRecordAppService.GetListAsync(input);
    }

    /// <summary>
    /// 获取指定车辆购买记录
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <returns>车辆购买记录详情</returns>
    [HttpGet("{id}")]
    public Task<VehiclePurchaseRecordDto> GetAsync(Guid id)
    {
        return _purchaseRecordAppService.GetAsync(id);
    }

    /// <summary>
    /// 创建车辆购买记录
    /// </summary>
    /// <param name="input">创建参数</param>
    /// <returns>创建后的车辆购买记录</returns>
    [HttpPost]
    public Task<VehiclePurchaseRecordDto> CreateAsync([FromBody] CreateVehiclePurchaseRecordDto input)
    {
        return _purchaseRecordAppService.CreateAsync(input);
    }

    /// <summary>
    /// 更新车辆购买记录
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的车辆购买记录</returns>
    [HttpPut("{id}")]
    public Task<VehiclePurchaseRecordDto> UpdateAsync(Guid id, [FromBody] UpdateVehiclePurchaseRecordDto input)
    {
        return _purchaseRecordAppService.UpdateAsync(id, input);
    }

    /// <summary>
    /// 删除车辆购买记录
    /// </summary>
    /// <param name="id">记录ID</param>
    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return _purchaseRecordAppService.DeleteAsync(id);
    }
}
