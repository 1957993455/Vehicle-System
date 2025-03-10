using System.Reflection.Emit;
using System.Threading.Tasks;
using Volo.Abp.Emailing;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using Volo.Abp.Localization;
using VehicleApp.Domain.Shared.Localization;

namespace VehicleApp.Domain.Settings;

public class VehicleAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {

        var host = context.GetOrNull(EmailSettingNames.Smtp.Host);
        if (host != null)
        {
            host.DefaultValue = "smtp.qq.com";
            host.DisplayName = L("DisplayName:Abp.Mailing.Smtp.Host");
                
          
        }
        var port = context.GetOrNull(EmailSettingNames.Smtp.Port);
        {
            port.DefaultValue = "465";
            port.DisplayName = L("DisplayName:Abp.Mailing.Smtp.Port");
        }

        var userName = context.GetOrNull(EmailSettingNames.Smtp.UserName);
        if (userName!= null)
        {
            userName.DefaultValue = "1957993455@qq.com";
            userName.DisplayName = L("DisplayName:Abp.Mailing.Smtp.UserName");
        }

        var password = context.GetOrNull(EmailSettingNames.Smtp.Password);
        if (password!= null)
        {
            // 这是加密还是不是加密的？
            password.DefaultValue = "rdcxerzbgwmjhjhf";
            password.DisplayName = L("DisplayName:Abp.Mailing.Smtp.Password");
            password.IsEncrypted = false;
        }

        var domain = context.GetOrNull(EmailSettingNames.Smtp.Domain);
        if (domain!= null)
        {
            domain.DefaultValue = "1957993455@qq.com";
            domain.DisplayName = L("DisplayName:Abp.Mailing.Smtp.Domain");
        }

        var useDefaultCredentials = context.GetOrNull(EmailSettingNames.Smtp.UseDefaultCredentials);
        if (useDefaultCredentials != null)
        {
            useDefaultCredentials.DefaultValue = "false";
            useDefaultCredentials.DisplayName = L("DisplayName:Abp.Mailing.Smtp.UseDefaultCredentials");
        }

        var enableSsl = context.GetOrNull(EmailSettingNames.Smtp.EnableSsl);
        if (enableSsl!= null)
        {
            enableSsl.DefaultValue = "true";
            enableSsl.DisplayName = L("DisplayName:Abp.Mailing.Smtp.EnableSsl");
        }

        var defaultFromAddress = context.GetOrNull(EmailSettingNames.DefaultFromAddress);
        if (defaultFromAddress!= null)
        {
            defaultFromAddress.DefaultValue = "1957993455@qq.com";
            defaultFromAddress.DisplayName = L("DisplayName:Abp.Mailing.DefaultFromAddress");
        }

        var defaultFromDisplayName = context.GetOrNull(EmailSettingNames.DefaultFromDisplayName);
        if (defaultFromDisplayName!= null)
        {
            defaultFromDisplayName.DefaultValue = "VehicleApp";
            defaultFromDisplayName.DisplayName = L("DisplayName:Abp.Mailing.DefaultFromDisplayName");
        }

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<VehicleAppResource>(name);
    }
}
