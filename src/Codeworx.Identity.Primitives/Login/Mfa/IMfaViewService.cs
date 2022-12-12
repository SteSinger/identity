﻿using System.Threading.Tasks;
using Codeworx.Identity.Model;

namespace Codeworx.Identity.Login.Mfa
{
    public interface IMfaViewService
    {
        Task<MfaLoginResponse> ShowLoginAsync(MfaLoginRequest request, string errorMessage = null);

        Task<SignInResponse> ProcessLoginAsync(MfaProcessLoginRequest request);
    }
}
