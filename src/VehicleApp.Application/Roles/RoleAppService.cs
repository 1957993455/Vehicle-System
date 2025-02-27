// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.Roles;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace VehicleApp.Application.Roles;

/// <summary>
/// 角色应用服务
/// </summary>
/// <param name="identityRoleRepository"></param>
public class RoleAppService(IIdentityRoleRepository identityRoleRepository) : ApplicationService, IRoleAppService
{
    public async Task UpdateIsDefaultAsync(Guid id, bool isDefault)
    {
        var role = await GetRoleAsync(id);
        if (role == null)
        {
            throw new BusinessException("Role not found.");
        }
        role.IsDefault = isDefault;
        await identityRoleRepository.UpdateAsync(role);
    }

    public async Task UpdateIsPublicAsync(Guid id, bool isPublic)
    {
        var role = await GetRoleAsync(id);
        if (role == null)
        {
            throw new BusinessException("Role not found.");
        }
        role.IsPublic = isPublic;
        await identityRoleRepository.UpdateAsync(role);
    }

    public async Task UpdateIsStaticAsync(Guid id, bool isStatic)
    {
        var role = await GetRoleAsync(id);

        if (role == null)
        {
            throw new BusinessException("Role not found.");
        }

        role.IsStatic = isStatic;
        await identityRoleRepository.UpdateAsync(role);
    }

    private async Task<IdentityRole> GetRoleAsync(Guid id)
    {
        var role = await identityRoleRepository.GetAsync(id);
        if (role == null)
        {
            throw new BusinessException("Role not found.");
        }

        return role;
    }
}
