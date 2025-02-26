using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using VehicleApp.Application.Contracts.User;
using Volo.Abp;
using Volo.Abp.Identity;

namespace VehicleApp.Application.User;

public class UserAppService(IIdentityUserRepository userRepository) : IUserAppService
{
    public async Task UpdatePhoneNumber(Guid userId, string phoneNumber)
    {
        var user = await userRepository.GetAsync(userId) ?? throw new UserFriendlyException($"Could not find user with id: {userId}.");
        user.SetPhoneNumber(phoneNumber, true);

        await userRepository.UpdateAsync(user, autoSave: true);
    }

    /// <summary>
    /// 更新状态
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="isEnabled"></param>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task UpdateStatus(Guid userId, bool isEnabled)
    {
        var user = await userRepository.GetAsync(userId) ?? throw new UserFriendlyException($"Could not find user with id: {userId}.");
        user.SetIsActive(isEnabled);
        await userRepository.UpdateAsync(user, autoSave: true);
    }
}
