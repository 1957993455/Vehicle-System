using System;

namespace VehicleApp.Application.Contracts.Order.Dtos;

public class CreateOrderInput
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
