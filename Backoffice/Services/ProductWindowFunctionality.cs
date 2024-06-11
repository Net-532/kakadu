using System.Collections.Generic;
using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Kakadu.Backoffice.Services;


namespace Kakadu.Backoffice.Views
{
    public class ProductManager : IManageable<Product>
    {
        private ProductService ProductServ;

        public ProductManager()
        {
            ProductServ = new ProductService(new ProductRepositoryDB());
        }

        public List<Product> LoadItems()
        {
            return ProductServ.GetAll();
        }

        public void AddItem(Product item)
        {
            ProductServ.Save(item);
        }

        public void DeleteItem(int Id)
        {
            ProductServ.DeleteById(Id);
        }

        public void EditItem(int itemId, Product newItem)
        {
            ProductServ.Update(itemId, newItem);
        }

    }
}
