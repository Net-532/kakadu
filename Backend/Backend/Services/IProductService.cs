using System.Collections.Generic;

namespace backend
{
    interface IProductService
    {
        List<Product> getAll();
        Product getById(int id);
        void save(Product product);
        void deleteById(int id);
        void update(int id, Product product);
    }
}