using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace VehicleApp.Application.Contracts.Roles;

public interface IRoleAppService : IApplicationService
{
    /// <summary>
    /// 更新是否默认状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isDefault"></param>
    /// <returns></returns>
    Task UpdateIsDefaultAsync(Guid id, bool isDefault);

    /// <summary>
    /// 更新是否公开状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isPublic"></param>
    /// <returns></returns>
    Task UpdateIsPublicAsync(Guid id, bool isPublic);

    /// <summary>
    /// 更新是否静态角色
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isStatic"></param>
    /// <returns></returns>
    Task UpdateIsStaticAsync(Guid id, bool isStatic);
}
