// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace VehicleApp.Domain.Vehicle
{
    public class VehiclePurchaseRecordDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<VehiclePurchaseRecordEntity, Guid> _vehiclePurchaseRecordRepository;

        public VehiclePurchaseRecordDataSeedContributor(IRepository<VehiclePurchaseRecordEntity, Guid> vehiclePurchaseRecordRepository)
        {
            _vehiclePurchaseRecordRepository = vehiclePurchaseRecordRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _vehiclePurchaseRecordRepository.GetCountAsync() > 0)
                return;

            List<VehiclePurchaseRecordEntity> vehiclePurchaseRecords = new List<VehiclePurchaseRecordEntity>();

            //穿件20条车辆购买记录
            for (int i = 1; i <= 20; i++)
            {
                 Guid vehicleId = Guid.NewGuid();
               DateTime purchaseDate = DateTime.Now.AddDays(-i);
              decimal purchasePrice = 10000 + i * 1000;
        string supplierName = "供应商" + i;
           string supplierPhoneNumber = "138" + i.ToString().PadLeft(8, '0');
        string paymentMethod =i==1? "微信" : i==2? "支付宝" : i==3? "银行卡" : i==4? "支付宝": "其他";
        string? remarks = $"备注{i}";


                var vehiclePurchaseRecord = new VehiclePurchaseRecordEntity(Guid.NewGuid(),vehicleId, purchaseDate, purchasePrice, supplierName, supplierPhoneNumber, paymentMethod, remarks);
                vehiclePurchaseRecords.Add(vehiclePurchaseRecord);
            }

            await _vehiclePurchaseRecordRepository.InsertManyAsync(vehiclePurchaseRecords);

        }
    }
}
