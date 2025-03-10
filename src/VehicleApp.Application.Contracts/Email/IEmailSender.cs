// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Application.Contracts.Email;

/// <summary>
/// 邮件发送接口
/// </summary>
public interface IEmailSenderAppService
{
    /// <summary>
    /// 发送邮件验证码
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task SendEmailCodeAsync(string email);
}
