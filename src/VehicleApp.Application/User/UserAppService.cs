using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VehicleApp.Application.Contracts.User;
using VehicleApp.Domain.Shared;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.MailKit;
using Volo.Abp.Settings;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace VehicleApp.Application.User;

public class UserAppService(
    IRepository<IdentityUser, Guid> userRepository,
    CurrentUser currentUser,
    UserManager<IdentityUser> userManager,
    IMailKitSmtpEmailSender emailSender) : IUserAppService
{

    [UnitOfWork]
    public async Task BatchDeleteUsers(IEnumerable<Guid> userIds)
    {
        // 参数基础验证
        if (userIds == null || !userIds.Any())
        {
            throw new ArgumentException("用户ID集合不能为空", nameof(userIds));
        }

        // 转换为可操作列表
        var userIdList = userIds.ToList();

        // 防止删除当前用户
        if (currentUser?.Id != null && userIdList.Contains(currentUser.Id.Value))
        {
            throw new BusinessException(IdentityErrorCodes.UserSelfDeletion,
                "不能删除当前登录用户");
        }

        // 获取实际存在的用户
        var allUsers = await userRepository.GetListAsync(u => userIdList.Contains(u.Id));

        if (allUsers.Count != userIdList.Count)
        {
            throw new BusinessException("未找到所有指定的用户");
        }

        foreach (var item in allUsers)
        {
            (await userManager.DeleteAsync(item)).CheckErrors();
        }
    }

    /// <summary>
    /// 更新头像
    /// </summary>
    /// <param name="id"></param>
    /// <param name="avatar"></param>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task UpdateAvatarAsync(Guid id, string avatar)
    {
        var user = await userRepository.GetAsync(id);
        if (user == null)
        {
            throw new UserFriendlyException("Could not find user with id: " + id);
        }

        var res = user.GetProperty(EntityPropertyExtensionConsts.User.Avatar);
        user.SetProperty(EntityPropertyExtensionConsts.User.Avatar, avatar);
        await userRepository.UpdateAsync(user, autoSave: true);

    }

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

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="email"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        await emailSender.SendAsync("1957993455@qq.com", subject, message);
    }
}
