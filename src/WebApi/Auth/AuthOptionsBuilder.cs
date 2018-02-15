using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nibo.Framework.WebApi.Auth
{
    public interface IAuthOptionsBuilderClient : IAuthOptionsBuilder
    {
        IAuthOptionsBuilderClient AsClient(string clientId);
        IAuthOptionsBuilderClient WithSecret(string clientSecret);
        IAuthOptionsBuilderPassport And();
    }
    public interface IAuthOptionsBuilderPassport : IAuthOptionsBuilder
    {
        IAuthOptionsBuilderPassport AuthenticateOn(string passportUrl);
        IAuthOptionsBuilderPassport WithJwtPassword(string password);
        IAuthOptionsBuilderReturnUrl And();
    }
    public interface IAuthOptionsBuilderReturnUrl : IAuthOptionsBuilder
    {
        IAuthOptionsBuilderReturnUrl ReturnToUrl(string returnUrl);
        IAuthOptionsBuilder And();
    }
    public interface IAuthOptionsBuilder
    {
        IAuthOptions Build();
    }
    public class AuthOptionsBuilder : IAuthOptionsBuilder, IAuthOptionsBuilderClient, IAuthOptionsBuilderPassport, IAuthOptionsBuilderReturnUrl
    {
        private string clientId;
        private string clientSecret;
        private string jwtPassword;
        private string passportUrl;
        private string returnUrl;

        public IAuthOptionsBuilderClient AsClient(string clientId)
        {
            this.clientId = clientId;
            return this;
        }

        public IAuthOptions Build()
        {
            var errors = new List<string>(5);
            if (string.IsNullOrWhiteSpace(this.clientId))
            {
                errors.Add("clientId");
            }

            if (string.IsNullOrWhiteSpace(this.clientSecret))
            {
                errors.Add("clientSecret");
            }

            if (string.IsNullOrWhiteSpace(this.jwtPassword))
            {
                errors.Add("jwtPassword");
            }

            if (string.IsNullOrWhiteSpace(this.passportUrl))
            {
                errors.Add("passportUrl");
            }

            if (string.IsNullOrWhiteSpace(this.returnUrl))
            {
                errors.Add("returnUrl");
            }

            if (errors.Any())
            {
                throw new AuthOptionsInvalidException(errors);
            }

            return new AuthOptions(this.clientId, this.clientSecret, this.jwtPassword, this.passportUrl, this.returnUrl);
        }

        public IAuthOptionsBuilderReturnUrl ReturnToUrl(string returnUrl)
        {
            this.returnUrl = returnUrl;
            return this;
        }

        public IAuthOptionsBuilderPassport AuthenticateOn(string passportUrl)
        {
            this.passportUrl = passportUrl;
            return this;
        }

        public IAuthOptionsBuilderClient WithSecret(string clientSecret)
        {
            this.clientSecret = clientSecret;
            return this;
        }

        IAuthOptionsBuilderPassport IAuthOptionsBuilderClient.And()
        {
            return this;
        }

        IAuthOptionsBuilderReturnUrl IAuthOptionsBuilderPassport.And()
        {
            return this;
        }

        IAuthOptionsBuilder IAuthOptionsBuilderReturnUrl.And()
        {
            return this;
        }

        IAuthOptionsBuilderPassport IAuthOptionsBuilderPassport.WithJwtPassword(string password)
        {
            this.jwtPassword = password;
            return this;
        }
    }
}