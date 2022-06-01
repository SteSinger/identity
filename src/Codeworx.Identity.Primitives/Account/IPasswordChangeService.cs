﻿using System.Threading.Tasks;
using Codeworx.Identity.Model;

namespace Codeworx.Identity.Account
{
    public interface IPasswordChangeService
    {
        Task<PasswordChangeResponse> ProcessChangePasswordAsync(ProcessPasswordChangeRequest request);

        Task<PasswordChangeViewResponse> ShowChangePasswordViewAsync(PasswordChangeRequest request);
    }
}