﻿namespace Codeworx.Identity.Model
{
    public interface IUser
    {
        string DefaultTenantKey { get; }

        string Identity { get; }

        string Name { get; }

        string PasswordHash { get; }

        bool ForceChangePassword { get; }
    }
}