using System.Collections.Generic;
using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backoffice.Services;


namespace Kakadu.Backoffice.Views
{
    public class ProductManager : IManageable<Product>
    {
        private IProductRepository ProductRep;

        public ProductManager()
        {
            ProductRep = new ProductRepositoryXML();
        }

        public List<Product> LoadItems()
        {
            return ProductRep.GetAll();
        }

        public void AddItem(Product item)
        {
            ProductRep.Save(item);
        }

        public void DeleteItem(int Id)
        {
            ProductRep.DeleteById(Id);
        }

        public void EditItem(int itemId, Product newItem)
        {
            ProductRep.Update(itemId, newItem);
        }

    }
}
