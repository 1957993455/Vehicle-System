using System;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace VehicleApp.Application.Contracts.Organization.Dtos;

/// <summary>
/// 创建组织单元输入
/// </summary>
public class CreateOrganizationUnitInput : IHasExtraProperties
{
    public virtual Guid? ParentId { get; set; }
    public required virtual string Code { get; set; }
    public required virtual string DisplayName { get; set; }
    public ExtraPropertyDictionary ExtraProperties { get; set; } = new();
}

