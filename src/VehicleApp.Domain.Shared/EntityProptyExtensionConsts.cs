using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Domain.Shared;

public static class EntityPropertyExtensionConsts
{
    public class User
    {
        public const string Avatar = nameof(Avatar);
        public const string RealName = nameof(RealName);
        public const string Address = nameof(Address);
        public const string Age = nameof(Age);
        public const string Sex = nameof(Sex);
        public const string IDCardNo = nameof(IDCardNo);
        public const string IsVerified = nameof(IsVerified);

        //国家
        public const string Country = nameof(Country);

        //区域
        public const string Area = nameof(Area);

        //是否认证

        //描述
        public const string Description = nameof(Description);
    }

    public class Role
    {
        public const string DisplayName = nameof(DisplayName);
    }
}
