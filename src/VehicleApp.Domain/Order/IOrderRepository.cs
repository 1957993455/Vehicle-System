using System;
using Volo.Abp.Domain.Repositories;
namespace VehicleApp.Domain.Order;

public interface IOrderRepository : IRepository<OrderAggregateRoot, Guid>
{
}
