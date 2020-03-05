﻿using Codeworx.Identity.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codeworx.Identity.Test
{
    public static class CodeworxIdentityTestAspNetCoreExtensions
    {
        public static IIdentityServiceBuilder UseTestSetup(this IIdentityServiceBuilder builder)
        {
            return builder.UserProvider<DummyUserService>()
                .PasswordValidator<DummyPasswordValidator>()
                .ReplaceService<IDefaultTenantService, DummyUserService>(ServiceLifetime.Scoped)
                .ReplaceService<ITenantService, DummyTenantService>(ServiceLifetime.Scoped)
                .ReplaceService<IClientService, DummyOAuthClientService>(ServiceLifetime.Scoped);
        }
    }
}