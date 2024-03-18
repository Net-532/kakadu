using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakadu.Backend.Repositories
{
    internal interface IProductRepository
    {
        List<Product> getAll();
        Product getById(int id);

        void save(Product product);
        void deleteById(int id);

        void update(int id, Product product);
    }
}
