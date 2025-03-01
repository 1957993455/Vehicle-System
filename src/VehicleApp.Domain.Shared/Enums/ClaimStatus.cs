// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Domain.Shared.Enums;

public enum ClaimStatusEnum
{
    /// <summary>
    /// 未申请理赔状态。表示事故发生后，相关人员还未向保险公司提交理赔申请。
    /// </summary>
    NotApplied = 1,

    /// <summary>
    /// 已申请理赔状态。表明相关人员已经向保险公司提交了理赔申请，保险公司开始处理该理赔案件。
    /// </summary>
    Applied = 2,

    /// <summary>
    /// 已赔付状态。意味着保险公司已经完成了理赔审核，并向申请人支付了相应的理赔款项，理赔流程结束。
    /// </summary>
    Settled = 4,

    /// <summary>
    /// 理赔被拒状态。即保险公司在审核理赔申请后，认为不符合理赔条件，拒绝了该理赔申请。
    /// </summary>
    Rejected = 8,
}
