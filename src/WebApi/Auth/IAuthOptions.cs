using System;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Nibo.Framework.WebApi.Auth
{
    public interface IAuthOptions
    {
        string ChallengeScheme { get; }
        string AuthorizationEndpoint { get; }
        string TokenEndpoint { get; }
        string UserInformationEndpoint { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        Action<ClaimActionCollection> MapClaims { get; }
    }
}