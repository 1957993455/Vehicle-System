using System;
using System.Collections.Generic;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Order.Dtos;

public class OrderDto : AuditedEntityDto<Guid>
{
    public Guid VehicleId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public DateTime? PickupTime { get; set; }
    public DateTime? ReturnTime { get; set; }
    public string Remarks { get; set; }
}

public class CreateOrderDto
{
    public Guid VehicleId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? PickupTime { get; set; }
    public DateTime? ReturnTime { get; set; }
    public string Remarks { get; set; }
    public string ItemName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}

public class UpdateOrderDto
{
    public string Remarks { get; set; }
}

public class GetOrderListInput : PagedAndSortedResultRequestDto
{
    public OrderStatus? Status { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? VehicleId { get; set; }
}