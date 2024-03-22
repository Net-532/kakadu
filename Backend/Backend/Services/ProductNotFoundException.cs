using System;

namespace Backend
{
    internal class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException(string message) : base(message) { }
    }
}
