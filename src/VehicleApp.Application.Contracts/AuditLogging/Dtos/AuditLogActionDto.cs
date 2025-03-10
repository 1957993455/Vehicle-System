using System;
using Volo.Abp.Data;

public class AuditLogActionDto
{
    /// <summary>
    /// 租户ID
    /// </summary>
    public virtual Guid? TenantId { get; set; }

    /// <summary>
    /// 审计日志ID
    /// </summary>
    public virtual Guid AuditLogId { get; set; }

    /// <summary>
    /// 服务名称
    /// </summary>
    public virtual string ServiceName { get; set; }

    /// <summary>
    /// 方法名称
    /// </summary>
    public virtual string MethodName { get; set; }

    /// <summary>
    /// 参数
    /// </summary>
    public virtual string Parameters { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    public virtual DateTime ExecutionTime { get; set; }

    /// <summary>
    /// 执行持续时间(毫秒)
    /// </summary>
    public virtual int ExecutionDuration { get; set; }

    /// <summary>
    /// 额外属性
    /// </summary>
    public virtual ExtraPropertyDictionary ExtraProperties { get; set; }
}
