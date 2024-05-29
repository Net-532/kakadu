using System;

namespace Kakadu.Backend.Services
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string message) : base(message) { }
    }
}
