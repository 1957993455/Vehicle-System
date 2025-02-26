namespace VehicleApp.Application.Contracts.Permissions;

public static class VehicleAppPermissions
{
    public const string GroupName = "VehicleApp";

    public static class Vehicle
    {
        public const string Default = GroupName + ".Vehicle";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string UpdateMileage = Default + ".UpdateMileage";
        public const string ChangeStatus = Default + ".ChangeStatus";
    }

    public static class MaintenanceRecord
    {
        public const string Default = GroupName + ".MaintenanceRecord";
        public const string Create = Default + ".Create";
        public const string View = Default + ".View";
    }

    public static class Store
    {
        public const string Default = GroupName + ".Store";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string ChangeStatus = Default + ".ChangeStatus";
        public const string Relocate = Default + ".Relocate";
    }

    public static class Organization
    {
        public const string Default = GroupName + ".Organization";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string ManageMembers = Default + ".ManageMembers";
    }
}