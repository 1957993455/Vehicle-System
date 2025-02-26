using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace VehicleApp.Application.Contracts.User;

public interface IUserAppService : IApplicationService
{
    /// <summary>
    /// 更新用户状态
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="isEnabled"></param>
    /// <returns></returns>
    Task UpdateStatus(Guid userId, bool isEnabled);

    /// <summary>
    /// 更新用户手机号
    /// </summary>
    Task UpdatePhoneNumber(Guid userId, string phoneNumber);

    /// <summary>}
    /// 批量删除用户
    /// </summary>
    Task BatchDeleteUsers(IEnumerable<Guid> userIds);

}
