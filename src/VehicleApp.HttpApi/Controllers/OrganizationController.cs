using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.Organization;
using VehicleApp.Application.Contracts.Organization.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.HttpApi.Controllers;

[ControllerName("organization")]
[Area("organization")]
[RemoteService]
[Route("api/organizations")]
public class OrganizationController(IOrganizationAppService organizationAppService) : VehicleAppController, IOrganizationAppService
{
    /// <summary>
    /// 创建组织单元
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<OrganizationUnitDto> CreateAsync([FromBody] CreateOrganizationUnitInput input)
    {
        var result = organizationAppService.CreateAsync(input);

        return result;
    }

    /// <summary>
    /// 删除组织单元
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await organizationAppService.DeleteAsync(id);
    }

    /// <summary>
    /// 获取组织单元
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<OrganizationUnitDto> GetAsync(Guid id)
    {
        return await organizationAppService.GetAsync(id);
    }

    /// <summary>
    /// 获取组织单元列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PagedResultDto<OrganizationUnitDto>> GetListAsync([FromQuery] GetOrganizationUnitInput input)
    {
        return await organizationAppService.GetListAsync(input);
    }

    /// <summary>
    /// 获取组织单元树
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("tree")]
    public async Task<List<OrganizationUnitDto>> GetOrganizationsTreeAsync(CancellationToken cancellationToken = default)
    {
        return await organizationAppService.GetOrganizationsTreeAsync(cancellationToken);
    }

    /// <summary>
    /// 更新组织单元
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<OrganizationUnitDto> UpdateAsync(Guid id, [FromBody] UpdateOrganizationUnitInput input)
    {
        return await organizationAppService.UpdateAsync(id, input);
    }

    /// <summary>
    /// 批量删除组织单元
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task DeleteAsync([FromBody] IEnumerable<Guid> ids)
    {
        await organizationAppService.DeleteAsync(ids);
    }
}
