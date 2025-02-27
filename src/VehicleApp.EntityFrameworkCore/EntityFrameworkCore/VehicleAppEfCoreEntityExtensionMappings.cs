using Microsoft.EntityFrameworkCore;
using VehicleApp.Domain.Shared;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace VehicleApp.EntityFrameworkCore.EntityFrameworkCore;

public static class VehicleAppEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        VehicleAppGlobalFeatureConfigurator.Configure();
        VehicleAppModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            //添加用户拓展属性
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<IdentityUser, string?>(
                    EntityPropertyExtensionConsts.User.RealName,
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(128);
                    }
                )
                .MapEfCoreProperty<IdentityUser, string?>(
                EntityPropertyExtensionConsts.User.Avatar, (entityBuilder, propertyBuilder) =>
                {
                })
                .MapEfCoreProperty<IdentityUser, SexEnum?>(
                EntityPropertyExtensionConsts.User.Sex, (entityBuilder, propertyBuilder) => { })
                .MapEfCoreProperty<IdentityUser, string?>(EntityPropertyExtensionConsts.User.IDCardNo, (e, p) =>
                {
                })
                .MapEfCoreProperty<IdentityUser, bool?>(EntityPropertyExtensionConsts.User.IsVerified, (e, p) => { })
                .MapEfCoreProperty<IdentityUser, int?>(EntityPropertyExtensionConsts.User.Age, (e, p) => { })
                .MapEfCoreProperty<IdentityUser, string?>(EntityPropertyExtensionConsts.User.Address, (e, p) => { })
                .MapEfCoreProperty<IdentityUser, string?>(EntityPropertyExtensionConsts.User.Country, (e, p) => { })
                .MapEfCoreProperty<IdentityUser, string?>(EntityPropertyExtensionConsts.User.Area, (e, p) => { })
                .MapEfCoreProperty<IdentityUser, string?>(EntityPropertyExtensionConsts.User.Description, (e, p) => { });

            //添加角色拓展属性
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<IdentityRole, string?>(
                    EntityPropertyExtensionConsts.Role.DisplayName,
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasMaxLength(256);
                    }
                );
        });
    }
}
