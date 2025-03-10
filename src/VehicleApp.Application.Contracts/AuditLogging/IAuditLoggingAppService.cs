using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace VehicleApp.Application.Contracts.AuditLogging;

/// <summary>
/// 审计日志应用服务接口
/// </summary>
public interface IAuditLoggingApplicationService : IApplicationService, ITransientDependency
{
    /// <summary>
    /// 获取审计日志
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<AuditLogDto>> GetAuditLogs(GetAuditLogsInput input);

    /// <summary>
    /// 获取审计日志操作
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    //Task<PagedResultDto<AuditLogActionDto>> GetAuditLogActions(GetAuditLogActionsInput input);

}
