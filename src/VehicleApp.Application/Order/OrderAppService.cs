using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Order;
using VehicleApp.Application.Contracts.Order.Dtos;
using VehicleApp.Domain.Order;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Application.Order;

public class OrderAppService(
    IRepository<OrderAggregateRoot, Guid> repository,
    OrderManager orderManager,
    IRepository<OrderDetailEntity, Guid> orderDetailRepository)
    :
        CrudAppService<
            OrderAggregateRoot,
            OrderDto,
            Guid,
            GetOrderListInput,
            CreateOrderDto,
            UpdateOrderDto>(repository),
        IOrderAppService
{
    public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
    {
        var order = await orderManager.CreateAsync(
            input.VehicleId,
            input.CustomerId,
            input.TotalAmount,
            input.PickupTime,
            input.ReturnTime,
            input.Remarks);

        await Repository.InsertAsync(order);

        // 创建订单明细

        var orderDetail = await orderManager.AddOrderDetailAsync(
            order,
            input.ItemName,
            input.UnitPrice,
            input.Quantity,
            input.Description);

        await orderDetailRepository.InsertAsync(orderDetail);

        return ObjectMapper.Map<OrderAggregateRoot, OrderDto>(order);
    }

    protected override async Task<IQueryable<OrderAggregateRoot>> CreateFilteredQueryAsync(GetOrderListInput input)
    {
        var query = await base.CreateFilteredQueryAsync(input);

        return query
            .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
            .WhereIf(input.CustomerId.HasValue, x => x.CustomerId == input.CustomerId)
            .WhereIf(input.VehicleId.HasValue, x => x.VehicleId == input.VehicleId);
    }
}