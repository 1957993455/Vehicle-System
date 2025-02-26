using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace VehicleApp.Application.Contracts.User;

public interface IUserAppService : IApplicationService
{
    Task UpdateStatus(Guid userId, bool isEnabled);

    Task UpdatePhoneNumber(Guid userId, string phoneNumber);
}