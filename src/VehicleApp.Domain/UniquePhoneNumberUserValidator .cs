using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace VehicleApp.Domain;

public class UniquePhoneNumberUserValidator : IUserValidator<Volo.Abp.Identity.IdentityUser>
{
    public const string PhoneNumberStartsWithZeroErrorCode = "PhoneNumberStartsWithZero";
    public const string NonNumericPhoneNumberErrorCode = "NonNumericPhoneNumber";
    public const string DuplicatePhoneNumberErrorCode = "DuplicatePhoneNumber";

    private readonly IRepository<Volo.Abp.Identity.IdentityUser, Guid> _userRepository;
    public UniquePhoneNumberUserValidator(
        IRepository<Volo.Abp.Identity.IdentityUser, Guid> userRepository)
    {
        _userRepository = userRepository;
    }

    public virtual async Task<IdentityResult> ValidateAsync(UserManager<Volo.Abp.Identity.IdentityUser> manager, Volo.Abp.Identity.IdentityUser user)
    {
        var errors = new List<IdentityError>();

        var phoneNumber = await manager.GetPhoneNumberAsync(user);

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return IdentityResult.Success;
        }

        CheckNotStartsWithZero(phoneNumber, errors);

        CheckIsNumeric(phoneNumber, errors);

        // PhoneNumber can be duplicated but confirmed PhoneNumber can't.
        if (user.PhoneNumberConfirmed)
        {
            await CheckIsNotDuplicateAsync(phoneNumber, manager, user, errors);
        }

        return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }

    protected virtual async Task CheckIsNotDuplicateAsync(string phoneNumber, UserManager<Volo.Abp.Identity.IdentityUser> userManager,
        Volo.Abp.Identity.IdentityUser user, List<IdentityError> errors)
    {
        Volo.Abp.Identity.IdentityUser? owner = await _userRepository.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

        if (owner != null &&
            !string.Equals(await userManager.GetUserIdAsync(owner), await userManager.GetUserIdAsync(user)))
        {
            errors.Add(new IdentityError
            {
                Code = DuplicatePhoneNumberErrorCode,
                Description = "手机号以被注册"
            });
        }
    }

    protected virtual void CheckIsNumeric(string phoneNumber, List<IdentityError> errors)
    {
        if (!phoneNumber.All(char.IsDigit))
        {
            errors.Add(new IdentityError
            {
                Code = NonNumericPhoneNumberErrorCode,
                Description = "手机号格式错误"
            });
        }
    }

    protected virtual void CheckNotStartsWithZero(string phoneNumber, List<IdentityError> errors)
    {
        if (phoneNumber.StartsWith("0"))
        {
            errors.Add(new IdentityError
            {
                Code = PhoneNumberStartsWithZeroErrorCode,
                Description = "手机号格式错误"
            });
        }
    }
}
