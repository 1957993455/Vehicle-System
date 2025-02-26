using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Organization.Dtos;

/// <summary>
/// 获取组织单元输入
/// </summary>
public class GetOrganizationUnitInput : ExtensiblePagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
