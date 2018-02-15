using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nibo.Framework.WebApi.Auth
{
    public class AuthOptionsInvalidException : Exception
    {
        const string MESSAGE = "O(s) parâmetro(s) {0} deve(m) ser preenchidos nas opções de autenticação.";
        public AuthOptionsInvalidException(IEnumerable<string> fields) : base(string.Format(MESSAGE, string.Join(",", fields)))
        {
        }
    }
}