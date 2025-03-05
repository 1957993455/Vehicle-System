using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.Order;
using VehicleApp.Application.Contracts.Order.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.HttpApi.Controllers;

/// <summary>
/// 订单详情API控制器
/// </summary>
[RemoteService]
[Route("api/order-details")]
[Authorize]
public class OrderDetailController : VehicleAppController
{
    private readonly IOrderDetailAppService _orderDetailAppService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="orderDetailAppService">订单详情应用服务</param>
    public OrderDetailController(IOrderDetailAppService orderDetailAppService)
    {
        _orderDetailAppService = orderDetailAppService;
    }

    /// <summary>
    /// 获取订单详情列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页后的订单详情列表</returns>
    [HttpGet]
    public Task<PagedResultDto<OrderDetailDto>> GetListAsync([FromQuery] GetOrderDetailListInput input)
    {
        return _orderDetailAppService.GetListAsync(input);
    }

    /// <summary>
    /// 获取指定订单的所有详情
    /// </summary>
    /// <param name="orderId">订单ID</param>
    /// <returns>订单详情列表</returns>
    [HttpGet("order/{orderId}")]
    public Task<List<OrderDetailDto>> GetByOrderIdAsync(Guid orderId)
    {
        return _orderDetailAppService.GetByOrderIdAsync(orderId);
    }

    /// <summary>
    /// 获取指定订单详情
    /// </summary>
    /// <param name="id">详情ID</param>
    /// <returns>订单详情</returns>
    [HttpGet("{id}")]
    public Task<OrderDetailDto> GetAsync(Guid id)
    {
        return _orderDetailAppService.GetAsync(id);
    }

    /// <summary>
    /// 创建订单详情
    /// </summary>
    /// <param name="input">创建参数</param>
    /// <returns>创建后的订单详情</returns>
    [HttpPost]
    public Task<OrderDetailDto> CreateAsync([FromBody] CreateOrderDetailDto input)
    {
        return _orderDetailAppService.CreateAsync(input);
    }

    /// <summary>
    /// 批量创建订单详情
    /// </summary>
    /// <param name="inputs">创建参数列表</param>
    /// <returns>创建后的订单详情列表</returns>
    [HttpPost("batch")]
    public Task<List<OrderDetailDto>> CreateManyAsync([FromBody] List<CreateOrderDetailDto> inputs)
    {
        return _orderDetailAppService.CreateManyAsync(inputs);
    }

    /// <summary>
    /// 更新订单详情
    /// </summary>
    /// <param name="id">详情ID</param>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的订单详情</returns>
    [HttpPut("{id}")]
    public Task<OrderDetailDto> UpdateAsync(Guid id, [FromBody] UpdateOrderDetailDto input)
    {
        return _orderDetailAppService.UpdateAsync(id, input);
    }

    /// <summary>
    /// 删除订单详情
    /// </summary>
    /// <param name="id">详情ID</param>
    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return _orderDetailAppService.DeleteAsync(id);
    }
}
