using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using VehicleApp.Application.Contracts.Organization.Dtos;
using Volo.Abp.Application.Services;
using System.Threading;

namespace VehicleApp.Application.Contracts.Organization;

/// <summary>
/// 组织单元应用服务
/// </summary>
public interface IOrganizationAppService : ICrudAppService<OrganizationUnitDto, Guid, GetOrganizationUnitInput, CreateOrganizationUnitInput, UpdateOrganizationUnitInput>
{
    /// <summary>
    /// 获取组织单元树
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<OrganizationUnitDto>> GetOrganizationsTreeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task DeleteAsync(IEnumerable<Guid> ids);
}
