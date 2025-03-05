using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleApp.Domain.Order;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore.Repositories;

public class EfCoreOrderRepository : EfCoreRepository<VehicleAppDbContext, OrderAggregateRoot, Guid>, IOrderRepository
{
    public EfCoreOrderRepository(IDbContextProvider<VehicleAppDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }



}
