using System;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Order.Dtos;

public class GetOrderListInput : PagedAndSortedResultRequestDto
{
    public OrderStatus? Status { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? VehicleId { get; set; }
}
