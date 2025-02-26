using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace VehicleApp.Domain.Shared;

public static class VehicleAppModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
        });
    }

    private static void ConfigureExistingProperties()
    {

    }

    private static void ConfigureExtraProperties()
    {
        ObjectExtensionManager.Instance.Modules()
           .ConfigureIdentity(identity =>
           {
               identity.ConfigureUser(user =>
               {
                   user.AddOrUpdateProperty<string>( //property type: string
                       EntityPropertyExtensionConsts.User.RealName, //property name
                       property =>
                       {
                           property.Attributes.Add(new StringLengthAttribute(64) { MinimumLength = 4 });
                       }
                   );
                   user.AddOrUpdateProperty<string>(EntityPropertyExtensionConsts.User.Avatar, property =>
                   {
                       property.Attributes.Add(new RequiredAttribute());
                   });
                   user.AddOrUpdateProperty<string>(EntityPropertyExtensionConsts.User.Address, property =>
                   {
                   });
                   user.AddOrUpdateProperty<SexEnum>(EntityPropertyExtensionConsts.User.Sex, property =>
                   {
                   });
                   user.AddOrUpdateProperty<string>(EntityPropertyExtensionConsts.User.IDCardNo, property =>
                   {
                       property.Attributes.Add(new StringLengthAttribute(18) { MinimumLength = 18 });
                   });
                   user.AddOrUpdateProperty<bool>(EntityPropertyExtensionConsts.User.IsVerified, property =>
                   {
                       property.Attributes.Add(new DefaultValueAttribute(false));
                   });
                   user.AddOrUpdateProperty<int>(EntityPropertyExtensionConsts.User.Age, property =>
                   {
                   });
               });
           });
    }
}