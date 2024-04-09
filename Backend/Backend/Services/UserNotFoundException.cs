using System;

namespace Kakadu.Backend.Services
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(string message) : base(message) { }
    }
}
