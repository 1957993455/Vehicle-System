using System;
using Volo.Abp.Data;

namespace VehicleApp.Application.Contracts.Organization.Dtos;

/// <summary>
/// 更新组织单元输入
/// </summary>
public class UpdateOrganizationUnitInput : IHasExtraProperties
{
    public required virtual string DisplayName { get; set; }

    public required virtual string Code { get; set; }

    public virtual Guid? ParentId { get; set; }
    public ExtraPropertyDictionary ExtraProperties { get; set; } = new();
}

