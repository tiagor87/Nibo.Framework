using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nibo.Framework.WebApi.Auth
{
    public class AuthOptionsInvalidException : Exception
    {
        public AuthOptionsInvalidException() : base("All fields in AuthOptions should be given")
        {
        }
    }
}