using System;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Nibo.Framework.WebApi.Auth
{
    public class AuthOptions : IAuthOptions
    {
        public AuthOptions(string challengeScheme, string clientId, string clientSecret, string authorizationEndpoint, string tokenEndpoint, string userInformationEndpoint, Action<ClaimActionCollection> mapClaims)
        {
            this.ChallengeScheme = challengeScheme;
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.AuthorizationEndpoint = authorizationEndpoint;
            this.TokenEndpoint = tokenEndpoint;
            this.UserInformationEndpoint = userInformationEndpoint;
            this.MapClaims = mapClaims;
        }

        public string ChallengeScheme { get; set; }
        public string AuthorizationEndpoint { get; private set; }
        public string TokenEndpoint { get; private set; }
        public string UserInformationEndpoint { get; private set; }
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public Action<ClaimActionCollection> MapClaims { get; private set; }

        internal static void IsValid(IAuthOptions authOptions)
        {
            if (string.IsNullOrWhiteSpace(authOptions.ChallengeScheme) ||
                string.IsNullOrWhiteSpace(authOptions.ClientId) ||
                string.IsNullOrWhiteSpace(authOptions.ClientSecret) ||
                string.IsNullOrWhiteSpace(authOptions.AuthorizationEndpoint) ||
                string.IsNullOrWhiteSpace(authOptions.TokenEndpoint) ||
                string.IsNullOrWhiteSpace(authOptions.UserInformationEndpoint))
            {
                throw new AuthOptionsInvalidException();
            }
        }
    }
}