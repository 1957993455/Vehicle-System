using VehicleApp.Application.Tests.Samples;
using VehicleApp.TestBase;
using Xunit;

namespace VehicleApp.EntityFrameworkCore.Tests.EntityFrameworkCore.Applications;

[Collection(VehicleAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<VehicleAppEntityFrameworkCoreTestModule>
{

}
