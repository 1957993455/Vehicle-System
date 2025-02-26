using Microsoft.Extensions.Localization;
using VehicleApp.Domain.Shared.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace VehicleApp.HttpApi.Host;

[Dependency(ReplaceServices = true)]
public class VehicleAppBrandingProvider(IStringLocalizer<VehicleAppResource> localizer) : DefaultBrandingProvider
{
    private readonly IStringLocalizer<VehicleAppResource> _localizer = localizer;

    public override string AppName => _localizer["AppName"];
}