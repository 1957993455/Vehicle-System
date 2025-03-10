using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.AuditLogging;
using Volo.Abp;

namespace VehicleApp.HttpApi.Controllers;

/// <summary>
/// 审计日志控制器
/// </summary>
[RemoteService]
[Area("AuditLog")]
[Route("api/audit-log")]
public class AuditLogController(IAuditLoggingApplicationService auditLoggingAppService) : VehicleAppController
{
    /// <summary>
    /// 获取审计日志
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAuditLogs(GetAuditLogsInput input)
    {
        var result = await auditLoggingAppService.GetAuditLogs(input);
        return Ok(result);
    }

    /// <summary>
    /// 获取审计日志操作
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    //[HttpGet("actions")]
    //public async Task<IActionResult> GetAuditLogActions(GetAuditLogActionsInput input)
    //{
    //    var result = await auditLoggingAppService.GetAuditLogActions(input);
    //    return Ok(result);
    //}

}
