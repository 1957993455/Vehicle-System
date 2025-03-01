using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.Store;
using VehicleApp.Application.Contracts.Store.Dtos;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.HttpApi.Controllers;

[RemoteService]
[Route("api/stores")]
[Authorize]
public class StoreController : VehicleAppController
{
    private readonly IStoreAppService _storeAppService;

    public StoreController(IStoreAppService storeAppService)
    {
        _storeAppService = storeAppService;
    }

    [HttpGet]
    public Task<PagedResultDto<StoreDto>> GetListAsync([FromQuery] GetListStoreInput input)
    {
        return _storeAppService.GetListAsync(input);
    }

    [HttpGet("{id}")]
    public Task<StoreDto> GetAsync(Guid id)
    {
        return _storeAppService.GetAsync(id);
    }

    [HttpPost]
    public Task<StoreDto> CreateAsync([FromBody] CreateStoreInput input)
    {
        return _storeAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public Task<StoreDto> UpdateAsync(Guid id, [FromBody] UpdateStoreInput input)
    {
        return _storeAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return _storeAppService.DeleteAsync(id);
    }

    [HttpPut("{id}/status")]
    public Task<StoreDto> ChangeStatusAsync(Guid id, [FromBody] StoreStatus newStatus)
    {
        return _storeAppService.ChangeStatusAsync(id, newStatus);
    }

    [HttpPut("{id}/location")]
    public Task<StoreDto> RelocateAsync(Guid id, [FromBody] RelocateStoreRequest request)
    {
        return _storeAppService.RelocateAsync(id, request.NewAddress, request.Longitude, request.Latitude);
    }
}
