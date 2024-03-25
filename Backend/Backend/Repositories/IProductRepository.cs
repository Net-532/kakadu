using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakadu.Backend.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int id);

        void Save(Product product);
        void DeleteById(int id);

        void Update(int id, Product product);
    }
}
