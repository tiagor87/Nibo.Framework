using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nibo.Framework.WebApi.Auth;

namespace Nibo.Framework.WebApi.Auth.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Authenticate(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = $"/api/auth/token?returnUrl={returnUrl}" });
        }

        [HttpGet("token")]
        public IActionResult GetToken(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                var token = this.HttpContext.GetTokenAsync("access_token").Result;
                return RedirectPermanent($"{returnUrl}?token={token}");
            }
            return Forbid();
        }
    }
}