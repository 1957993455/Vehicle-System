using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace VehicleApp.Domain.Vehicle;

/// <summary>
/// 事故记录数据种子Contributor
/// </summary>
public class AccidentRecordContributor : IDataSeedContributor, ITransientDependency
{
    protected IRepository<AccidentRecordEntity> AccidentRecordRepository { get; }
    protected IIdentityUserRepository IdentityUserRepository { get; }
    protected IGuidGenerator GuidGenerator { get; }

    public AccidentRecordContributor(
        IRepository<AccidentRecordEntity> accidentRecordRepository,
        IIdentityUserRepository identityUserRepository,
        IGuidGenerator guidGenerator)
    {
        AccidentRecordRepository = accidentRecordRepository;
        IdentityUserRepository = identityUserRepository;
        GuidGenerator = guidGenerator;
    }

    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await AccidentRecordRepository.GetCountAsync() > 0)
        {
            return;
        }

        IdentityUser user = new IdentityUser(GuidGenerator.Create(), "admin", "admin@admin.com");
        await IdentityUserRepository.InsertAsync(user);

        List<AccidentRecordEntity> accidentRecords = new List<AccidentRecordEntity>();

        for (int i = 1; i <= 10; i++)
        {
            Guid id = Guid.NewGuid();
            DateTime accidentDate = DateTime.Now.AddDays(-i);
            string description = $"事故描述 {i}";
            decimal repairCost = i * 1000;
            Guid vehicleId = user.Id;
            string accidentLocation = $"事故地点 {i}";
            ClaimStatusEnum claimStatus = (ClaimStatusEnum)(i % 4);
            Guid driverId = Guid.NewGuid();
            string driverLicenseType = $"驾驶证类型 {i % 3 + 1}";
            DateTime reportDate = accidentDate.AddHours(1);
            string handlingDepartment = $"处理部门 {i % 2 + 1}";
            string? insurancePolicyNumber = $"保单号 {i}";
            string? insuranceCompany = $"保险公司 {i % 3 + 1}";
            bool driverViolation = i % 2 == 0;

            AccidentRecordEntity record = new AccidentRecordEntity(
                id, accidentDate, description, repairCost, vehicleId,
                accidentLocation, claimStatus, driverId, driverLicenseType,
                reportDate, handlingDepartment, insurancePolicyNumber,
                insuranceCompany, driverViolation
            );
            accidentRecords.Add(record);
        }

        await AccidentRecordRepository.InsertManyAsync(accidentRecords);
    }
}
