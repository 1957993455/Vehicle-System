using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using VehicleApp.Application.Contracts.Email;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace VehicleApp.Application.Email;

public class EmailSenderAppService : IEmailSenderAppService, ITransientDependency
{
    protected IEmailSender EmailSender { get; }

    protected IDistributedCache<string> DistributedCache { get; }


    public EmailSenderAppService(IEmailSender emailSender, IDistributedCache<string> cache)
    {
        EmailSender = emailSender;
        DistributedCache = cache;
    }

    /// <summary>
    /// 发送邮件验证码
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task SendEmailCodeAsync(string email)
    {
        // 生成6位随机验证码
        var code = Random.Shared.Next(100000, 999999).ToString();
        // 将验证码存入缓存
        await DistributedCache.SetAsync(email, code, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10)
        });
        // 发送邮件
        await EmailSender.SendAsync(email, "验证码", $"您的验证码是：{code}");
    }
}
