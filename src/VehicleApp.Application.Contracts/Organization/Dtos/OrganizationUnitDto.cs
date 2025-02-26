using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Organization.Dtos;

public class OrganizationUnitDto : AuditedEntityDto<Guid>
{
    public virtual Guid? ParentId { get; set; }

    public virtual string Code { get; set; } = null!;

    public virtual string DisplayName { get; set; } = null!;

    public ICollection<OrganizationUnitDto> Children { get; set; } = [];
}
