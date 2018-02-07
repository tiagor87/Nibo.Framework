namespace Nibo.Framework.WebApi.Auth
{
    public class AuthOptions : IAuthOptions
    {
        public AuthOptions(string clientId, string clientSecret, string password, string passportUrl, string returnUrl)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.JwtPassword = password;
            this.PassportUrl = passportUrl;
            this.ReturnUrl = returnUrl;
        }

        public string PassportUrl { get; private set; }

        public string JwtPassword { get; private set; }

        public string ClientId { get; private set; }

        public string ClientSecret { get; private set; }

        public string ReturnUrl { get; private set; }
    }
}