﻿using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Codeworx.Identity.Configuration.Internal
{
    public static class CodeworxIdentityPrimitivesInternalIdentityServiceBuilderExtensions
    {
        public static void Register<TService, TImplementation>(this IIdentityServiceBuilder builder, ServiceLifetime lifetime, Func<IServiceProvider, TImplementation> factory = null)
         where TService : class
         where TImplementation : class, TService
        {
            var config = builder.ServiceCollection.FirstOrDefault(p => p.ServiceType == typeof(TService));

            if (config != null)
            {
                builder.ServiceCollection.Remove(config);
            }

            if (factory == null)
            {
                config = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);
            }
            else
            {
                config = new ServiceDescriptor(typeof(TService), factory, lifetime);
            }

            builder.ServiceCollection.Add(config);
        }

        public static void RegisterScoped<TService, TImplementation>(this IIdentityServiceBuilder builder, Func<IServiceProvider, TImplementation> factory)
            where TService : class
            where TImplementation : class, TService
        {
            builder.Register<TService, TImplementation>(ServiceLifetime.Scoped, factory);
        }

        public static void RegisterSingleton<TService, TImplementation>(this IIdentityServiceBuilder builder, Func<IServiceProvider, TImplementation> factory)
            where TService : class
            where TImplementation : class, TService
        {
            builder.Register<TService, TImplementation>(ServiceLifetime.Singleton, factory);
        }
    }
}