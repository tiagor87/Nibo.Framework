using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace Nibo.Framework.WebApi.Auth
{
    public class AuthOptionsBuilder
    {
        private string clientId;
        private string clientSecret;
        private string authorizationEndpoint;
        private string tokenEndpoint;
        private string userInformationEndpoint;
        private string challengeScheme;
        private IDictionary<string, string> mapClaim;

        public AuthOptionsBuilder()
        {
            this.mapClaim = new Dictionary<string, string>();
        }

        public AuthOptionsBuilder ForScheme(string challengeScheme)
        {
            this.challengeScheme = challengeScheme;
            return this;
        }
        public AuthOptionsBuilder AsClient(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            return this;
        }
        public AuthOptionsBuilder WithAuthorizationEndpoint(string authorizationEndpoint)
        {
            this.authorizationEndpoint = authorizationEndpoint;
            return this;
        }

        public AuthOptionsBuilder WithTokenEndpoint(string tokenEndpoint)
        {
            this.tokenEndpoint = tokenEndpoint;
            return this;
        }

        public AuthOptionsBuilder WithUserInformationEndpoint(string userInformationEndpoint)
        {
            this.userInformationEndpoint = userInformationEndpoint;
            return this;
        }

        public AuthOptionsBuilder MapJsonKeyForClaim(string claimType, string jsonKey)
        {
            this.mapClaim.Add(claimType, jsonKey);
            return this;
        }

        public IAuthOptions Build()
        {
            return new AuthOptions(
                this.challengeScheme,
                this.clientId,
                this.clientSecret,
                this.authorizationEndpoint,
                this.tokenEndpoint,
                this.userInformationEndpoint,
                claims =>
                {
                    mapClaim
                        .AsParallel()
                        .ForAll(map => claims.MapJsonKey(map.Key, map.Value));
                });
        }
    }
}