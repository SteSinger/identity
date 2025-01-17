﻿using System;

namespace Codeworx.Identity.OAuth
{
    public class AuthorizationTokenOptions
    {
        public int Length { get; set; } = 10;

        public double ExpirationInSeconds { get; set; } = TimeSpan.FromMinutes(10).TotalSeconds;
    }
}