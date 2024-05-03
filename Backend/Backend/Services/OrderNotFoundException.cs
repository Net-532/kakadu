using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakadu.Backend.Services
{
    public class OrderNotFoundException : ApplicationException
    {
        public OrderNotFoundException(string message) : base(message) { }
    }
}
