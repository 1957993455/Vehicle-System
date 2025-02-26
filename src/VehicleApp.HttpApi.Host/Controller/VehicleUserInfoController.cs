using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenIddict.Abstractions;
using VehicleApp.Domain.Shared;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.OpenIddict.Controllers;
using Volo.Abp.Security.Claims;

namespace VehicleApp.HttpApi.Host.Controller;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(UserInfoController))]
public class VehicleUserInfoController : UserInfoController
{
    protected override async Task<Dictionary<string, object>> GetUserInfoClaims()
    {
        var user = await UserManager.GetUserAsync(User);
        if (user == null)
        {
            return null;
        }

        var claims = new Dictionary<string, object>(StringComparer.Ordinal)
        {
            // Note: the "sub" claim is a mandatory claim and must be included in the JSON response.
            [OpenIddictConstants.Claims.Subject] = await UserManager.GetUserIdAsync(user)
        };

        if (User.HasScope(OpenIddictConstants.Scopes.Profile))
        {
            claims[AbpClaimTypes.TenantId] = user.TenantId!;
            claims[OpenIddictConstants.Claims.PreferredUsername] = user.UserName;
            claims[OpenIddictConstants.Claims.FamilyName] = user.Surname;
            claims[OpenIddictConstants.Claims.GivenName] = user.Name;
            claims["avatar"] = user.GetProperty<string>(EntityPropertyExtensionConsts.User.Avatar) ?? string.Empty;
            claims["gender"] = user.GetProperty<SexEnum>(EntityPropertyExtensionConsts.User.Sex) == SexEnum.Male ? "男" : "女";
            claims["nickname"] = user.GetProperty<string>(EntityPropertyExtensionConsts.User.RealName) ?? string.Empty;
            claims["registrationDate"] = user.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
            claims["country"] = user.GetProperty<string>(EntityPropertyExtensionConsts.User.Country) ?? string.Empty;
            claims["area"] = user.GetProperty<string>(EntityPropertyExtensionConsts.User.Area) ?? string.Empty;
            claims["address"] = user.GetProperty<string>(EntityPropertyExtensionConsts.User.Address) ?? string.Empty;
            claims["description"] = user.GetProperty<string>(EntityPropertyExtensionConsts.User.Description) ?? string.Empty;
            claims["isVerified"] = user.GetProperty<bool>(EntityPropertyExtensionConsts.User.IsVerified) ? "已认证" : "未认证";
        }

        if (User.HasScope(OpenIddictConstants.Scopes.Email))
        {
            claims[OpenIddictConstants.Claims.Email] = await UserManager.GetEmailAsync(user);
            claims[OpenIddictConstants.Claims.EmailVerified] = await UserManager.IsEmailConfirmedAsync(user);
        }

        if (User.HasScope(OpenIddictConstants.Scopes.Phone))
        {
            claims[OpenIddictConstants.Claims.PhoneNumber] = await UserManager.GetPhoneNumberAsync(user);
            claims[OpenIddictConstants.Claims.PhoneNumberVerified] = await UserManager.IsPhoneNumberConfirmedAsync(user);
        }

        if (User.HasScope(OpenIddictConstants.Scopes.Roles))
        {
            claims[OpenIddictConstants.Claims.Role] = await UserManager.GetRolesAsync(user);
        }

        return claims;
    }
}