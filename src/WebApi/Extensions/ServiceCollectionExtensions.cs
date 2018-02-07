using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nibo.Framework.WebApi.Auth;

namespace Nibo.Framework.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNiboAuthentication(this IServiceCollection services, IAuthOptions options)
        {
            services.AddSingleton<IAuthOptions>(options);
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new NiboTokenValidationParameters(options.JwtPassword);
                }
            );
            return services;
        }

        public static IServiceCollection AddNiboAuthentication(this IServiceCollection services, Func<IAuthOptionsBuilderClient, IAuthOptions> configOptions)
        {
            return services.AddNiboAuthentication(configOptions(new AuthOptionsBuilder()));
        }
    }
}
