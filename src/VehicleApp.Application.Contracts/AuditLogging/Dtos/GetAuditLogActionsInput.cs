using System;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.AuditLogging;

public class GetAuditLogActionsInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string HttpMethod { get; set; }
    public string Url { get; set; }
    public string UserName { get; set; }
    public string ApplicationName { get; set; }
}
