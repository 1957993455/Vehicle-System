using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Application.Contracts.AuditLogging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Application.AuditLogging;

public class AuditLoggingApplicationService : ApplicationService, IAuditLoggingApplicationService
{
    protected IAuditLogRepository AuditLogRepository { get; }

    public AuditLoggingApplicationService(IAuditLogRepository auditLogRepository) => AuditLogRepository = auditLogRepository;

    //protected IRepository<AuditLogAction, Guid> AuditLogActionRepository { get; }

    //public AuditLoggingApplicationService(IAuditLogRepository auditLogRepository,
    //    IRepository<AuditLogAction, Guid> auditLogActionRepository)
    //{
    //    AuditLogRepository = auditLogRepository;
    //    AuditLogActionRepository = auditLogActionRepository;
    //}

    /// <summary>
    /// 获取审计日志
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<AuditLogDto>> GetAuditLogs(GetAuditLogsInput input)
    {
        var query = await BuildAuditLogQueryable(input);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<AuditLogDto>(result.Count, ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(result));
    }

    /// <summary>
    /// 构建审计日志查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private async Task<IQueryable<AuditLog>> BuildAuditLogQueryable(GetAuditLogsInput input)
    {
        var query = await AuditLogRepository.GetQueryableAsync();
        query = query
                     .WhereIf(input.StartTime.HasValue, x => x.ExecutionTime >= input.StartTime.Value)
                     .WhereIf(input.EndTime.HasValue, x => x.ExecutionTime <= input.EndTime.Value)
                     .WhereIf(!string.IsNullOrEmpty(input.HttpMethod), x => x.HttpMethod == input.HttpMethod)
                     .WhereIf(!string.IsNullOrEmpty(input.Url), x => x.Url == input.Url)
                     .WhereIf(!string.IsNullOrEmpty(input.UserName), x => x.UserName == input.UserName)
                     .WhereIf(!string.IsNullOrEmpty(input.ApplicationName), x => x.ApplicationName == input.ApplicationName)
                     .WhereIf(!string.IsNullOrEmpty(input.ClientIpAddress), x => x.ClientIpAddress == input.ClientIpAddress)
                     .WhereIf(!string.IsNullOrEmpty(input.CorrelationId), x => x.CorrelationId == input.CorrelationId);
        return query.OrderBy(x => x.ExecutionTime)
                   .PageBy(input.SkipCount, input.MaxResultCount);
    }

    /// <summary>
    /// 获取审计日志操作
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    //public async Task<PagedResultDto<AuditLogActionDto>> GetAuditLogActions(GetAuditLogActionsInput input)
    //{
    //    var query = await BuildAuditLogActionQueryable(input);
    //    var result = await AsyncExecuter.ToListAsync(query);
    //    return new PagedResultDto<AuditLogActionDto>(result.Count, ObjectMapper.Map<List<AuditLogAction>, List<AuditLogActionDto>>(result));
    //}

    ///// <summary>
    ///// 构建审计日志操作查询
    ///// </summary>
    ///// <param name="input"></param>
    ///// <returns></returns>
    //private async Task<IQueryable<AuditLogAction>> BuildAuditLogActionQueryable(GetAuditLogActionsInput input)
    //{
    //    var query = await AuditLogActionRepository.GetQueryableAsync();
    //    query = query.WhereIf(input.StartTime.HasValue, x => x.ExecutionTime >= input.StartTime.Value)
    //                 .WhereIf(input.EndTime.HasValue, x => x.ExecutionTime <= input.EndTime.Value)
    //                 .WhereIf(!string.IsNullOrEmpty(input.HttpMethod), x => x.MethodName == input.HttpMethod);
    //    return query.OrderBy(x => x.ExecutionTime)
    //               .PageBy(input.MaxResultCount, input.SkipCount);
    //}

}
