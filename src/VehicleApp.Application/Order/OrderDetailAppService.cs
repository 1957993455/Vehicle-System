using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Order;
using VehicleApp.Application.Contracts.Order.Dtos;
using VehicleApp.Domain.Order;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Application.Order;

/// <summary>
/// 订单详情应用服务实现
/// </summary>
public class OrderDetailAppService :
    CrudAppService<
        OrderDetailEntity,
        OrderDetailDto,
        Guid,
        GetOrderDetailListInput,
        CreateOrderDetailDto,
        UpdateOrderDetailDto>,
    IOrderDetailAppService
{
    private readonly OrderManager _orderManager;
    private readonly IRepository<OrderAggregateRoot, Guid> _orderRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public OrderDetailAppService(
        IRepository<OrderDetailEntity, Guid> repository,
        OrderManager orderManager,
        IRepository<OrderAggregateRoot, Guid> orderRepository)
        : base(repository)
    {
        _orderManager = orderManager;
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// 创建订单详情
    /// </summary>
    public override async Task<OrderDetailDto> CreateAsync(CreateOrderDetailDto input)
    {
        var order = await _orderRepository.GetAsync(input.OrderId);

        var detail = await _orderManager.AddOrderDetailAsync(
            order,
            input.ItemName,
            input.UnitPrice,
            input.Quantity,
            input.Description);

        await Repository.InsertAsync(detail);

        return ObjectMapper.Map<OrderDetailEntity, OrderDetailDto>(detail);
    }

    /// <summary>
    /// 批量创建订单详情
    /// </summary>
    public async Task<List<OrderDetailDto>> CreateManyAsync(List<CreateOrderDetailDto> inputs)
    {
        if (!inputs.Any())
        {
            return new List<OrderDetailDto>();
        }

        var orderId = inputs.First().OrderId;
        var order = await _orderRepository.GetAsync(orderId);
        var details = new List<OrderDetailEntity>();

        foreach (var input in inputs)
        {
            var detail = await _orderManager.AddOrderDetailAsync(
                order,
                input.ItemName,
                input.UnitPrice,
                input.Quantity,
                input.Description);

            details.Add(detail);
        }

        await Repository.InsertManyAsync(details);

        return ObjectMapper.Map<List<OrderDetailEntity>, List<OrderDetailDto>>(details);
    }

    /// <summary>
    /// 获取指定订单的所有详情
    /// </summary>
    public async Task<List<OrderDetailDto>> GetByOrderIdAsync(Guid orderId)
    {
        var details = await Repository.GetListAsync(x => x.OrderId == orderId);
        return ObjectMapper.Map<List<OrderDetailEntity>, List<OrderDetailDto>>(details);
    }

    /// <summary>
    /// 创建查询过滤器
    /// </summary>
    protected override async Task<IQueryable<OrderDetailEntity>> CreateFilteredQueryAsync(GetOrderDetailListInput input)
    {
        var query = await base.CreateFilteredQueryAsync(input);

        return query
            .WhereIf(input.OrderId.HasValue, x => x.OrderId == input.OrderId);
    }
}
