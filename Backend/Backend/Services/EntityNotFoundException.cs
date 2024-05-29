using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakadu.Backend.Services
{
    internal class EntityNotFoundException: ApplicationException
    {
        public EntityNotFoundException(string message) : base(message) { }
    }
}
