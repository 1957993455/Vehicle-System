using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Domain.Store;

public class StoreDataSeedContributor(IRepository<StoreAggregateRoot, Guid> storeRepository) : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await storeRepository.GetCountAsync() > 0)
        {
            return;
        }

        var stores = new List<StoreAggregateRoot>();

        for (int i = 1; i <= 20; i++)
        {
            var name = $"品牌{i} {RandomLocation()}旗舰店";
            var storeCode = $"STORE_{RandomRegionCode()}_{RandomLocationCode()}_{i:000}";
            var fullAddress = $"{RandomCity()} {RandomDistrict()} {RandomStreet()}";
            var location = new GeoLocationValueObject(RandomDouble(), RandomDouble());
            var regionId = Guid.NewGuid();
            var store = new StoreAggregateRoot(name, storeCode, fullAddress, location, regionId);
            store.SetBusinessHours("9:00-18:00");

            stores.Add(store);
        }

        await storeRepository.InsertManyAsync(stores);
    }

    private string RandomLocation()
    {
        string[] locations = { "朝阳", "昌平", "大兴", "丰台", "顺义", "通州", "怀柔" };
        return locations[new Random().Next(locations.Length)];
    }

    private string RandomRegionCode()
    {
        string[] regionCodes = { "BJ", "SH", "GZ", "SZ", "CD", "WH", "XM" };
        return regionCodes[new Random().Next(regionCodes.Length)];
    }

    private string RandomLocationCode()
    {
        string[] locationCodes = { "CY", "CP", "DX", "FT", "SY", "TZ", "HR" };
        return locationCodes[new Random().Next(locationCodes.Length)];
    }

    private string RandomCity()
    {
        string[] cities = { "北京市", "上海市", "广州市", "深圳市", "成都市", "武汉市", "厦门市" };
        return cities[new Random().Next(cities.Length)];
    }

    private string RandomDistrict()
    {
        string[] districts = { "朝阳区", "昌平区", "大兴区", "丰台区", "顺义区", "通州区", "怀柔区" };
        return districts[new Random().Next(districts.Length)];
    }

    private string RandomStreet()
    {
        string[] streets = { "建国路88号华贸中心B1层", "王府井大街1号", "天河路100号", "南山区科技园", "高新区软件园", "江汉路1号", "思明南路100号" };
        return streets[new Random().Next(streets.Length)];
    }

    private double RandomDouble()
    {
        var random = new Random();
        return random.NextDouble() * (180 - (-180)) + (-180); // 随机生成一个-180到180之间的经度或纬度
    }
}
