using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Order.Dtos;
using Volo.Abp.Application.Services;

namespace VehicleApp.Application.Contracts.Order;

/// <summary>
/// 订单详情应用服务接口
/// </summary>
public interface IOrderDetailAppService : ICrudAppService<
    OrderDetailDto,
    Guid,
    GetOrderDetailListInput,
    CreateOrderDetailDto,
    UpdateOrderDetailDto>
{
    /// <summary>
    /// 获取指定订单的所有详情
    /// </summary>
    /// <param name="orderId">订单ID</param>
    /// <returns>订单详情列表</returns>
    Task<List<OrderDetailDto>> GetByOrderIdAsync(Guid orderId);

    /// <summary>
    /// 批量创建订单详情
    /// </summary>
    /// <param name="inputs">创建参数列表</param>
    /// <returns>创建的订单详情列表</returns>
    Task<List<OrderDetailDto>> CreateManyAsync(List<CreateOrderDetailDto> inputs);
}
