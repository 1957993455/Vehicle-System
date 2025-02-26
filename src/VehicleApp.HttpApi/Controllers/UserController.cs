using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp;
using Asp.Versioning;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using VehicleApp.Application.Contracts.User;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;

namespace VehicleApp.HttpApi.Controllers;

[RemoteService]
[Route("api/identity/users")]
[Area(IdentityRemoteServiceConsts.ModuleName)]
[ControllerName("User")]
public class UserController(IUserAppService userAppService) : VehicleAppController
{
    /// <summary>
    /// 修改用户手机号
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="phoneNumber">新手机号</param>
    /// <returns>更新后的用户信息</returns>
    [HttpPut("{id}/phone-number/{phoneNumber}")]
    public virtual async Task UpdatePhoneNumberAsync(
        [Required] Guid id,
        [Required][RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "手机号格式不正确")] string phoneNumber)
    {
        await userAppService.UpdatePhoneNumber(id, phoneNumber);
    }

    /// <summary>
    /// 修改用户状态
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="status">是否激活</param>
    /// <returns>更新后的用户信息</returns>
    [HttpPut("{id}/status/{status}")]
    public virtual async Task<IActionResult> ChangeUserStatusAsync(
        [Required] Guid id,
        [Required] bool status)
    {
        await userAppService.UpdateStatus(id, status);
        return Ok();
    }
}
