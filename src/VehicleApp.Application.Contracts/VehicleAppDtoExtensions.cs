using System;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace VehicleApp.Application.Contracts;

public static class VehicleAppDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                .AddOrUpdateProperty<GetIdentityUsersInput, Guid?>(
                    "OrganizationUnitId"
                )
                .AddOrUpdateProperty<GetIdentityUsersInput, string?>(
                    "OrganizationUnitName"
                );
        });
    }
}
