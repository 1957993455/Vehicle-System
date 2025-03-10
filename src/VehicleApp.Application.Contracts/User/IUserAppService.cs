using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace VehicleApp.Application.Contracts.User;

public interface IUserAppService : IApplicationService, ITransientDependency
{
    /// <summary>
    /// �����û�״̬
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="isEnabled"></param>
    /// <returns></returns>
    Task UpdateStatus(Guid userId, bool isEnabled);

    /// <summary>
    /// �����û��ֻ���
    /// </summary>
    Task UpdatePhoneNumber(Guid userId, string phoneNumber);

    /// <summary>}
    /// ����ɾ���û�
    /// </summary>
    Task BatchDeleteUsers(IEnumerable<Guid> userIds);

    Task UpdateAvatarAsync(Guid id, string avatar);

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="email"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendEmailAsync(string email, string subject, string message);
}
