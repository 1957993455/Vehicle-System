using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;
using VehicleApp.Domain.Shared;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Controllers;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;
using Identityuser = Volo.Abp.Identity.IdentityUser;

namespace VehicleApp.HttpApi.Controllers;

[IgnoreAntiforgeryToken]
[ApiExplorerSettings(IgnoreApi = true)]
public class SmsTokenController : AbpOpenIdDictControllerBase, ITokenExtensionGrant
{
    public const string ExtensionGrantName = "phone_number";

    public string Name => ExtensionGrantName;

    public async Task<IActionResult> HandleAsync(ExtensionGrantContext context)
    {
        // You can get a new token using any of the following methods based on your business.
        // They are just examples. You can implement your own logic here.

        return await HandleUserAccessTokenAsync(context);
    }

    public async Task<IActionResult> HandleUserAccessTokenAsync(ExtensionGrantContext context)
    {
        var userToken = context.Request.GetParameter("token").ToString();

        if (string.IsNullOrEmpty(userToken))
        {
            return new ForbidResult(
                new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                properties: new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidRequest
                }!));
        }

        // We will validate the user token
        // The Token is issued by the OpenIddict server, So we can validate it using the introspection endpoint

        var transaction = await context.HttpContext.RequestServices.GetRequiredService<IOpenIddictServerFactory>().CreateTransactionAsync();
        transaction.EndpointType = OpenIddictServerEndpointType.Introspection;
        transaction.Request = new OpenIddictRequest
        {
            ClientId = context.Request.ClientId,
            ClientSecret = context.Request.ClientSecret,
            Token = userToken
        };

        var notification = new OpenIddictServerEvents.ProcessAuthenticationContext(transaction);
        var dispatcher = context.HttpContext.RequestServices.GetRequiredService<IOpenIddictServerDispatcher>();
        await dispatcher.DispatchAsync(notification);

        if (notification.IsRejected)
        {
            return new ForbidResult(
                new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                properties: new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = notification.Error ?? OpenIddictConstants.Errors.InvalidRequest,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = notification.ErrorDescription,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorUri] = notification.ErrorUri
                }));
        }

        var principal = notification.GenericTokenPrincipal;
        if (principal == null)
        {
            return new ForbidResult(
                new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                properties: new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = notification.Error ?? OpenIddictConstants.Errors.InvalidRequest,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = notification.ErrorDescription,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorUri] = notification.ErrorUri
                }));
        }

        // We have validated the user token and got the user id

        var userId = principal.FindUserId();
        var userManager = context.HttpContext.RequestServices.GetRequiredService<IdentityUserManager>();
        var user = await userManager.GetByIdAsync(userId.Value);
        var userClaimsPrincipalFactory = context.HttpContext.RequestServices.GetRequiredService<IUserClaimsPrincipalFactory<Volo.Abp.Identity.IdentityUser>>();
        var claimsPrincipal = await userClaimsPrincipalFactory.CreateAsync(user);

        // Prepare the scopes
        var scopes = GetScopes(context);

        claimsPrincipal.SetScopes(scopes);
        claimsPrincipal.SetResources(await GetResourcesAsync(context, scopes));
        await context.HttpContext.RequestServices.GetRequiredService<AbpOpenIddictClaimsPrincipalManager>().HandleAsync(context.Request, principal);
        return new Microsoft.AspNetCore.Mvc.SignInResult(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, claimsPrincipal);
    }


    protected async Task<IActionResult> HandleUserApiKeyAsync(ExtensionGrantContext context)
    {
        var userApiKey = context.Request.GetParameter("user_api_key").ToString();

        if (string.IsNullOrEmpty(userApiKey))
        {
            return new ForbidResult(
                new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                properties: new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidRequest
                }!));
        }

        // Here we can validate the user API key and get the user id
        if (false) // Add your own logic here
        {
            // If the user API key is invalid
            return new ForbidResult(
                new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                properties: new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidRequest
                }!));
        }

        // Add your own logic to get the user by API key
        var userManager = context.HttpContext.RequestServices.GetRequiredService<IdentityUserManager>();
        var user = await userManager.FindByNameAsync("admin");
        if (user == null)
        {
            return new ForbidResult(
                new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                properties: new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidRequest
                }!));
        }

        // Create a principal for the user
        var userClaimsPrincipalFactory = context.HttpContext.RequestServices.GetRequiredService<IUserClaimsPrincipalFactory<Volo.Abp.Identity.IdentityUser>>();
        var claimsPrincipal = await userClaimsPrincipalFactory.CreateAsync(user);

        // Prepare the scopes
        var scopes = GetScopes(context);

        claimsPrincipal.SetScopes(scopes);
        claimsPrincipal.SetResources(await GetResourcesAsync(context, scopes));
        await context.HttpContext.RequestServices.GetRequiredService<AbpOpenIddictClaimsPrincipalManager>().HandleAsync(context.Request, claimsPrincipal);
        return new Microsoft.AspNetCore.Mvc.SignInResult(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, claimsPrincipal);
    }

    private ImmutableArray<string> GetScopes(ExtensionGrantContext context)
    {
        // Prepare the scopes
        // The scopes must be defined in the OpenIddict server

        // If you want to get the scopes from the request, you have to add `scope` parameter in the request
        // scope: AbpAPI profile roles email phone offline_access

        //var scopes = context.Request.GetScopes();

        // If you want to set the scopes here, you can use the following code
        var scopes = new[] { "VehicleApp_App", "profile", "roles", "email", "phone", "offline_access" }.ToImmutableArray();

        return scopes;
    }

    private async Task<IEnumerable<string>> GetResourcesAsync(ExtensionGrantContext context, ImmutableArray<string> scopes)
    {
        var resources = new List<string>();
        if (!scopes.Any())
        {
            return resources;
        }

        await foreach (var resource in context.HttpContext.RequestServices.GetRequiredService<IOpenIddictScopeManager>().ListResourcesAsync(scopes))
        {
            resources.Add(resource);
        }
        return resources;
    }
}
