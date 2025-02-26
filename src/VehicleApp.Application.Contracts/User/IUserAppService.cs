using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace VehicleApp.Application.Contracts.User;

public interface IUserAppService : IApplicationService
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

}
