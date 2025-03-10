using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Store.Dtos;

public class GetListStoreInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
    public StoreStatus? Status { get; set; }
}
