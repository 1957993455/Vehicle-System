using System;
using System.Collections.Generic;

namespace VehicleApp.Application.Contracts.Store.Dtos;

public class UpdateStoreInput
{
    public string Name { get; set; }
    public string BusinessHours { get; set; }
    public Guid? ManagerId { get; set; }
    public List<string> Tags { get; set; }
    public string Description { get; set; }
}
