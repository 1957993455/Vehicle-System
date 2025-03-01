using System.Threading.Tasks;
using VehicleApp.Domain.Vehicle;
using VehicleApp.TestBase;
using Xunit;

namespace VehicleApp.EntityFrameworkCore.Tests.EntityFrameworkCore.Samples;

[Collection(VehicleAppTestConsts.CollectionDefinitionName)]
public class VehicleRepositoryTests : VehicleAppEntityFrameworkCoreTestBase
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleRepositoryTests() => _vehicleRepository = GetRequiredService<IVehicleRepository>();

    [Fact]
    public async Task Should_Get_All_Vehicles()
    {
        // Act
        var vehicles = await _vehicleRepository.GetListAsync();

        // Assert
        Assert.NotEmpty(vehicles);
    }
}
