using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Nibo.Framework.WebApi.Auth
{
    public class NiboTokenValidationParameters : TokenValidationParameters
    {
        public NiboTokenValidationParameters(string password)
        {
            this.ValidateIssuerSigningKey = true;
            this.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(password));
            this.ValidateIssuer = false;
            this.ValidateAudience = false;
            this.ValidateLifetime = true;
            this.ClockSkew = TimeSpan.Zero;
        }
    }
}