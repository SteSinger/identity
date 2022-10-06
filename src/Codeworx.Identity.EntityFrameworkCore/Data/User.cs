﻿using System.Collections.Generic;
using Codeworx.Identity.Login;
using Codeworx.Identity.Model;

namespace Codeworx.Identity.EntityFrameworkCore.Data
{
    public class User : IUser
    {
        public string DefaultTenantKey { get; set; }

        public string Identity { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public bool ForceChangePassword { get; set; }

        public IReadOnlyList<string> LinkedProviders { get; set; }

        public int FailedLoginCount { get; set; }

        public bool ConfirmationPending { get; set; }

        public bool HasMfaRegistration { get; set; }

        public AuthenticationMode AuthenticationMode { get; set; }
    }
}