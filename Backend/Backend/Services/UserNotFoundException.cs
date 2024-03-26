using System;

namespace Kakadu.Backend.Services
{
    internal class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(string message) : base(message) { }
    }
}
