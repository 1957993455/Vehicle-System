using VehicleApp.Domain.Tests.Samples;
using VehicleApp.TestBase;
using Xunit;

namespace VehicleApp.EntityFrameworkCore.Tests.EntityFrameworkCore.Domains;

[Collection(VehicleAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<VehicleAppEntityFrameworkCoreTestModule>
{

}
