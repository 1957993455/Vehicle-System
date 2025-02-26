using VehicleApp.Domain.Shared.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace VehicleApp.Application.Contracts.Permissions;

public class VehicleAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var vehicleGroup = context.AddGroup(VehicleAppPermissions.GroupName, L("Permission:VehicleApp"));

        // 车辆权限
        var vehiclePermission = vehicleGroup.AddPermission(
            VehicleAppPermissions.Vehicle.Default,
            L("Permission:Vehicle"));

        vehiclePermission.AddChild(
            VehicleAppPermissions.Vehicle.Create,
            L("Permission:Vehicle.Create"));
        vehiclePermission.AddChild(
            VehicleAppPermissions.Vehicle.Update,
            L("Permission:Vehicle.Update"));
        vehiclePermission.AddChild(
            VehicleAppPermissions.Vehicle.Delete,
            L("Permission:Vehicle.Delete"));
        vehiclePermission.AddChild(
            VehicleAppPermissions.Vehicle.UpdateMileage,
            L("Permission:Vehicle.UpdateMileage"));
        vehiclePermission.AddChild(
            VehicleAppPermissions.Vehicle.ChangeStatus,
            L("Permission:Vehicle.ChangeStatus"));

        // 维护记录权限
        var maintenancePermission = vehicleGroup.AddPermission(
            VehicleAppPermissions.MaintenanceRecord.Default,
            L("Permission:MaintenanceRecord"));

        maintenancePermission.AddChild(
            VehicleAppPermissions.MaintenanceRecord.Create,
            L("Permission:MaintenanceRecord.Create"));
        maintenancePermission.AddChild(
            VehicleAppPermissions.MaintenanceRecord.View,
            L("Permission:MaintenanceRecord.View"));

        // 门店权限
        var storePermission = vehicleGroup.AddPermission(
            VehicleAppPermissions.Store.Default,
            L("Permission:Store"));

        storePermission.AddChild(
            VehicleAppPermissions.Store.Create,
            L("Permission:Store.Create"));
        storePermission.AddChild(
            VehicleAppPermissions.Store.Update,
            L("Permission:Store.Update"));
        storePermission.AddChild(
            VehicleAppPermissions.Store.Delete,
            L("Permission:Store.Delete"));
        storePermission.AddChild(
            VehicleAppPermissions.Store.ChangeStatus,
            L("Permission:Store.ChangeStatus"));
        storePermission.AddChild(
            VehicleAppPermissions.Store.Relocate,
            L("Permission:Store.Relocate"));

        // 组织权限
        var orgPermission = vehicleGroup.AddPermission(
            VehicleAppPermissions.Organization.Default,
            L("Permission:Organization"));

        orgPermission.AddChild(
            VehicleAppPermissions.Organization.Create,
            L("Permission:Organization.Create"));
        orgPermission.AddChild(
            VehicleAppPermissions.Organization.Update,
            L("Permission:Organization.Update"));
        orgPermission.AddChild(
            VehicleAppPermissions.Organization.Delete,
            L("Permission:Organization.Delete"));
        orgPermission.AddChild(
            VehicleAppPermissions.Organization.ManageMembers,
            L("Permission:Organization.ManageMembers"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<VehicleAppResource>(name);
    }
}