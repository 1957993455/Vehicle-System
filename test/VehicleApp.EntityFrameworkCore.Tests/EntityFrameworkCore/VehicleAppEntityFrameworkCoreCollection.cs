using VehicleApp.TestBase;
using Xunit;

namespace VehicleApp.EntityFrameworkCore.Tests.EntityFrameworkCore;

[CollectionDefinition(VehicleAppTestConsts.CollectionDefinitionName)]
public class VehicleAppEntityFrameworkCoreCollection : ICollectionFixture<VehicleAppEntityFrameworkCoreFixture>
{

}
