using System;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Order.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VehicleApp.Application.Contracts.Order;

public interface IOrderAppService : ICrudAppService<
    OrderDto,
    Guid,
    GetOrderListInput,
    CreateOrderInput,
    UpdateOrderInput>
{
}
