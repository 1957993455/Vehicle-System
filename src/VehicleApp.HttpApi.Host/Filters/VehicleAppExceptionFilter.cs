using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Validation;
using Volo.Abp.Domain.Entities;

namespace VehicleApp.HttpApi.Host.Filters;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(AbpExceptionFilter))]
public class VehicleAppExceptionFilter : AbpExceptionFilter
{
    protected override async Task HandleAndWrapException(ExceptionContext context)
    {
        LoggerException(context);
        await DefaultHandlerAsync(context);
    }

    private void LoggerException(ExceptionContext context)
    {
        var exceptionHandlingOptions = context.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
        var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
        exceptionToErrorInfoConverter.Convert(context.Exception, options =>
        {
            options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
            options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
        });

        var logger = context.GetService<ILogger<AbpExceptionFilter>>(NullLogger<AbpExceptionFilter>.Instance)!;
        var logLevel = context.Exception.GetLogLevel();
        logger.LogException(context.Exception, logLevel);
    }

    /// <summary>
    /// 默认异常处理
    /// </summary>
    private async Task DefaultHandlerAsync(ExceptionContext context)
    {
        var exceptionHandlingOptions = context.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
        var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
        var remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
        {
            options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
            options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
        });

        var statusCode = (int)context.GetRequiredService<IHttpExceptionStatusCodeFinder>()
            .GetStatusCode(context.HttpContext, context.Exception);

        var (message, code) = context.Exception switch
        {
            AbpAuthorizationException => ("未授权访问", 401),
            UserFriendlyException ex => (ex.Message, int.TryParse(ex.Code, out var c) ? c : 400),
            BusinessException ex => (ex.Message, int.TryParse(ex.Code, out var c) ? c : 400),
            AbpValidationException ex => (ex.ValidationErrors.JoinAsString(","), 400),
            EntityNotFoundException => ("请求的资源不存在", 404),
            _ => (remoteServiceErrorInfo.Message ?? "服务器错误", statusCode)
        };

        var result = new WrapResult<RemoteServiceErrorInfo>();
        result.SetFail(message, code);
        result.Data = null;

        if (context.Exception is AbpAuthorizationException authException)
        {
            await context.HttpContext.RequestServices
                .GetRequiredService<IAbpAuthorizationExceptionHandler>()
                .HandleAsync(authException, context.HttpContext);
        }

        if (context.Exception is BusinessException businessException)
        {
            context.HttpContext.Response.StatusCode = 400;
        }
        else
        {
            context.HttpContext.Response.StatusCode = code;
        }
        context.Result = new ObjectResult(result);
    }
}
