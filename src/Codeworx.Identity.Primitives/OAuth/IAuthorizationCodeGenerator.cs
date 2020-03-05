﻿using System.Threading.Tasks;

namespace Codeworx.Identity.OAuth
{
    public interface IAuthorizationCodeGenerator
    {
        Task<string> GenerateCode(AuthorizationRequest request, int length);
    }
}