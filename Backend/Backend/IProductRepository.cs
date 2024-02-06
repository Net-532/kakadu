using backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
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
