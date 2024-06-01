using System;

namespace Kakadu.Backend.Services
{
    internal class EntityNotFoundException: ApplicationException
    {
        public EntityNotFoundException(string message) : base(message) { }
    }
}
