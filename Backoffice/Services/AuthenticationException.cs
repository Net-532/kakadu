using System;

namespace Kakadu.Backoffice.Services
{
    public class AuthenticationException : ApplicationException
    {
        public AuthenticationException(string message) : base(message) { }
    }
}