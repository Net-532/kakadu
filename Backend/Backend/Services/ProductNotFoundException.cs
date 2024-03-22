using System;

namespace Kakadu.Backend.Services
{
    internal class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException(string message) : base(message) { }
    }
}
