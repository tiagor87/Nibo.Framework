using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nibo.Framework.WebApi.Auth;

namespace Nibo.Framework.WebApi.Auth.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthOptions options;

        public AuthController(IAuthOptions options)
        {
            this.options = options;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Redirect($"{this.options.PassportUrl}/Authorize?client_id={this.options.ClientId}&response_type=code&state=&redirect_uri=https://{Request.Host.Value}{Request.Path}/callback");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            SignOut(JwtBearerDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpGet("callback")]
        public IActionResult Callback([FromQuery] string code)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", $"https://{Request.Host.Value}{Request.Path}"),
                    new KeyValuePair<string, string>("client_id", this.options.ClientId),
                    new KeyValuePair<string, string>("client_secret", this.options.ClientSecret),
                });
                var response = client.PostAsync($"{this.options.PassportUrl}/token", content).Result;
                var responseJson = response.Content.ReadAsStringAsync().Result;
                var token = JsonConvert.DeserializeObject<Token>(responseJson);
                var handler = new JwtSecurityTokenHandler();
                var user = handler.ValidateToken(token.AccessToken, new NiboTokenValidationParameters(this.options.JwtPassword), out SecurityToken validatedToken);

                SignIn(user, JwtBearerDefaults.AuthenticationScheme);

                return Redirect($"{this.options.ReturnUrl}?token={token.AccessToken}");
            }
        }
    }
}