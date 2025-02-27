using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.Roles;
using Volo.Abp;
using Volo.Abp.Identity;

namespace VehicleApp.HttpApi.Controllers;

/// <summary>
/// ��ɫ������
/// </summary>
[RemoteService]
[Microsoft.AspNetCore.Mvc.Route("api/identity/roles")]
[Area(IdentityRemoteServiceConsts.ModuleName)]
[ControllerName("Role")]
public class RoleController : VehicleAppController, IRoleAppService
{
    protected IIdentityUserAppService UserAppService { get; }
    protected IRoleAppService RoleAppService { get; }

    public RoleController(IIdentityUserAppService userAppService, IRoleAppService roleAppService)
    {
        UserAppService = userAppService;
        RoleAppService = roleAppService;
    }

    /// <summary>
    /// ��ȡ��ɫ����
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{userId}/role-names")]
    public async Task<IEnumerable<string>> GetRoleNamesByUserIdAsync(Guid userId)
    {
        Volo.Abp.Application.Dtos.ListResultDto<IdentityRoleDto> resultDto = await UserAppService.GetRolesAsync(userId);

        return resultDto.Items.Select(x => x.Name);
    }

    /// <summary>
    /// ���½�ɫĬ��ֵ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isDefault"></param>
    /// <returns></returns>
    [HttpPut("{id}/is-default/{isDefault}")]
    public async Task UpdateIsDefaultAsync(Guid id, bool isDefault)
    {
        await RoleAppService.UpdateIsDefaultAsync(id, isDefault);
    }

    /// <summary>
    /// ���½�ɫ����
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isPublic"></param>
    /// <returns></returns>
    [HttpPut("{id}/is-public/{isPublic}")]
    public async Task UpdateIsPublicAsync(Guid id, bool isPublic)
    {
        await RoleAppService.UpdateIsPublicAsync(id, isPublic);
    }

    /// <summary>
    /// ���½�ɫ��̬
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isStatic"></param>
    /// <returns></returns>
    [HttpPut("{id}/is-static/{isStatic}")]
    public async Task UpdateIsStaticAsync(Guid id, bool isStatic)
    {
        await RoleAppService.UpdateIsStaticAsync(id, isStatic);
    }
}
