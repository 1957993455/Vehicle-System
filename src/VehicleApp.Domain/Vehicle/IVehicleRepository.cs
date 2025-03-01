using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Domain.Vehicle;

public interface IVehicleRepository : IRepository<VehicleAggregateRoot, Guid>
{
    Task<List<VehicleAggregateRoot>> GetListAsync(int skipCount = 0, int maxResultCount = int.MinValue, string? sorting = null, string? filter = null, bool includeDetails = false, VehicleStatus? status = null, CancellationToken cancellationToken = default);
}
