using System;
using System.Collections.Generic;

namespace VehicleApp.HttpApi.Controllers;

public class AddMemberRequest
{
    public Guid UserId { get; set; }
    public List<string> Roles { get; set; }
}