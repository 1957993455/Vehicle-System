using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Contracts.User;
using Volo.Abp;
using Volo.Abp.Settings;

namespace VehicleApp.HttpApi.Controllers;

[Route("api/email")]
[RemoteService(Name = "Email")]
public class EmailController : VehicleAppController
{

    protected IUserAppService UserAppService { get; }

    public EmailController(IUserAppService userAppService)
    {
        UserAppService = userAppService;
    }

    [AllowAnonymous]
    [HttpPost("send")]
    public async Task<IActionResult> SendEmail(string email, string subject, string message)
    {
        await UserAppService.SendEmailAsync(email, subject, message);
        return Ok();
    }




}
