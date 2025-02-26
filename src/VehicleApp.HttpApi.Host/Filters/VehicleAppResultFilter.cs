using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using VehicleApp.Domain.Shared;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace VehicleApp.HttpApi.Host.Filters;

public class VehicleAppResultFilter(IJsonSerializer jsonSerializer) : IResultFilter, ITransientDependency
{
    // 通过构造函数注入依赖项，提高可测试性

    public void OnResultExecuting(ResultExecutingContext context)
    {
        // 跳过不需要处理的请求类型
        if (ShouldSkipResultWrapping(context))
        {
            return;
        }

        // 处理结果包装逻辑
        var resultObject = GetResultData(context.Result);
        var statusCode = context.HttpContext.Response.StatusCode;
        if (statusCode > 200 && statusCode < 300)
        {
            statusCode = 200;
        }

        var wrappedResult = new WrapResult<object>
        {
            Success = IsSuccessStatusCode(statusCode),
            Code = statusCode,
            Data = resultObject,
            Message = GetMessageByStatusCode(statusCode)
        };

        context.Result = CreateJsonResult(wrappedResult);
    }

    public void OnResultExecuted(ResultExecutedContext context)
    { }

    #region Private Methods

    private object? GetResultData(IActionResult result)
    {
        return result switch
        {
            ObjectResult objectResult => objectResult.Value,
            JsonResult jsonResult => jsonResult.Value,
            ContentResult contentResult =>
                contentResult.ContentType == "application/json"
                    ? jsonSerializer.Deserialize<object>(contentResult.Content ?? string.Empty)
                    : contentResult.Content,
            _ => null
        };
    }

    private bool ShouldSkipResultWrapping(ResultExecutingContext context)
    {
        // 跳过 Swagger 请求
        if (context.HttpContext.Request.Headers["Accept"].ToString().Contains("application/json") &&
            context.HttpContext.Request.Path.StartsWithSegments("/swagger"))
        {
            return true;
        }

        // 跳过 ABP 配置请求
        if (context.HttpContext.Request.Path.StartsWithSegments("/api/abp/application-configuration"))
        {
            return true;
        }

        // 跳过 WebSocket 请求
        if (context.HttpContext.WebSockets.IsWebSocketRequest)
        {
            return true;
        }

        // 跳过 OpenIddict 的 token 端点
        if (context.HttpContext.Request.Path.StartsWithSegments("/connect/token"))
        {
            return true;
        }

        // 跳过其他特定结果类型
        return context.ActionDescriptor.IsPageAction()
               || context.Result is FileResult
               || context.Result is ChallengeResult
               || context.Result is ForbidResult
               || context.Result is SignInResult
               || context.ActionDescriptor.HasWrapResultAttribute();
    }

    private bool IsSuccessStatusCode(int statusCode)
    {
        return statusCode >= 200 && statusCode < 300;
    }

    private string GetMessageByStatusCode(int statusCode)
    {
        return statusCode switch
        {
            200 => "请求成功",
            400 => "参数验证失败",
            401 => "未授权访问",
            403 => "禁止访问",
            404 => "资源未找到",
            500 => "服务器内部错误",
            _ => "请求处理完成"
        };
    }

    private IActionResult CreateJsonResult(WrapResult<object> result)
    {
        return new ContentResult
        {
            Content = jsonSerializer.Serialize(result),
            ContentType = "application/json",
            StatusCode = 200 // 保持HTTP 200，实际状态码在包装体中体现
        };
    }

    #endregion Private Methods
}

// 扩展方法类
public static class ActionDescriptorExtensions
{
    public static bool HasWrapResultAttribute(this ActionDescriptor actionDescriptor)
    {
        if (actionDescriptor is ControllerActionDescriptor controllerDescriptor)
        {
            var controllerAttributes = controllerDescriptor.ControllerTypeInfo.GetCustomAttributes(true);
            var actionAttributes = controllerDescriptor.MethodInfo.GetCustomAttributes(true);

            return controllerAttributes.OfType<NotWrapResultAttribute>().Any()
                   || actionAttributes.OfType<NotWrapResultAttribute>().Any();
        }
        return false;
    }
}
