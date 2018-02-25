using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Nibo.Framework.WebApi.Auth;

namespace Nibo.Framework.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOAuthAuthentication(this IServiceCollection services, IAuthOptions authOptions)
        {
            AuthOptions.IsValid(authOptions);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = authOptions.ChallengeScheme;
            })
            .AddCookie()
            .AddOAuth(authOptions.ChallengeScheme, options =>
            {
                options.ClientId = authOptions.ClientId;
                options.ClientSecret = authOptions.ClientSecret;
                options.CallbackPath = new PathString($"/api/auth/signin-{authOptions.ChallengeScheme}");

                options.AuthorizationEndpoint = authOptions.AuthorizationEndpoint;
                options.TokenEndpoint = authOptions.TokenEndpoint;
                options.UserInformationEndpoint = authOptions.UserInformationEndpoint;

                options.SaveTokens = true;

                authOptions.MapClaims(options.ClaimActions);

                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                        context.RunClaimActions(user);
                    }
                };
            });
            return services;
        }

        public static IServiceCollection AddNiboAuthentication(this IServiceCollection services, Func<AuthOptionsBuilder, IAuthOptions> configOptions)
        {
            var builder = new AuthOptionsBuilder();
            return services.AddOAuthAuthentication(configOptions(builder));
        }
    }
}
