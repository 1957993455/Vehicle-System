using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Order;
using VehicleApp.Application.Contracts.Order.Dtos;
using VehicleApp.Domain.Order;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace VehicleApp.Application.Order;

public class OrderAppService(
    IRepository<OrderAggregateRoot, Guid> repository,
    OrderManager orderManager,
    IRepository<OrderDetailEntity, Guid> orderDetailRepository,
    IdentityUserManager userManager,
    IRepository<IdentityUser, Guid> userRepository,
    IdentityRoleManager roleManager)
    :
        CrudAppService<
            OrderAggregateRoot,
            OrderDto,
            Guid,
            GetOrderListInput,
            CreateOrderInput,
            UpdateOrderInput>(repository),
        IOrderAppService
{
    public override async Task<OrderDto> CreateAsync(CreateOrderInput input)
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


    public async override Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListInput input)
    {
        await CheckGetListPolicyAsync();

        //判断有没有是客户
        var customerRole = await roleManager.FindByNameAsync("Customer");
        if (customerRole == null)
        {
            return new PagedResultDto<OrderDto>(0, new List<OrderDto>());
        }
        // 获取所有客户ID列表
        var customers = await userManager.GetUsersInRoleAsync(customerRole.Name);
        var customerIds = customers.Select(u => u.Id).ToList();
        var customerDict = customers.ToDictionary(u => u.Id, u => u.UserName);
        IQueryable<OrderAggregateRoot> query = await CreateFilteredQueryAsync(input);
        // 使用包含客户ID列表的简单查询
        query = query.Where(x => customerIds.Contains(x.CustomerId));
        var totalCount = await AsyncExecuter.CountAsync(query);
        var entities = new List<OrderAggregateRoot>();
        var entityDtos = new List<OrderDto>();
        if (totalCount > 0)
        {
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            entities = await AsyncExecuter.ToListAsync(query);
            entityDtos = await MapToGetListOutputDtosAsync(entities);
        }

        // 使用字典查找来设置客户名称
        foreach (var item in entityDtos)
        {
            if (customerDict.TryGetValue(item.CustomerId, out var customerName))
            {
                item.CustomerName = customerName;
            }
        }

        return new PagedResultDto<OrderDto>(totalCount, entityDtos);
    }
}
