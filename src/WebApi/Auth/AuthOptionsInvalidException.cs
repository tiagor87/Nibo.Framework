using System;

namespace Nibo.Framework.WebApi.Auth
{
    public class AuthOptionsInvalidException : Exception
    {
        const string MESSAGE = "Todos os parâmetros devem ser preenchidos nas opções de autenticação.";
        public AuthOptionsInvalidException() : base(MESSAGE)
        {

        }
    }
}