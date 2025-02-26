namespace VehicleApp.HttpApi.Controllers;

public class RelocateStoreRequest
{
    public string NewAddress { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}