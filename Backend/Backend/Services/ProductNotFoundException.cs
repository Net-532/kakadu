using System;

namespace Kakadu.Backend.Services
{
    public class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException(string message) : base(message) { }
    }
}
