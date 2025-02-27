using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace VehicleApp.Application.Contracts.Roles;

public interface IRoleAppService : IApplicationService
{
    /// <summary>
    /// �����Ƿ�Ĭ��״̬
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isDefault"></param>
    /// <returns></returns>
    Task UpdateIsDefaultAsync(Guid id, bool isDefault);

    /// <summary>
    /// �����Ƿ񹫿�״̬
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isPublic"></param>
    /// <returns></returns>
    Task UpdateIsPublicAsync(Guid id, bool isPublic);

    /// <summary>
    /// �����Ƿ�̬��ɫ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isStatic"></param>
    /// <returns></returns>
    Task UpdateIsStaticAsync(Guid id, bool isStatic);
}
