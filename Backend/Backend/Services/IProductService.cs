using Kakadu.Backend.Entities;
using System.Collections.Generic;

namespace Kakadu.Backend.Services
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        void Save(Product product);
        void DeleteById(int id);
        void Update(int id, Product product);
    }
}