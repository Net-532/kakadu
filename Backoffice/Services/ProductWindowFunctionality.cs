using System.Collections.Generic;
using Kakadu.Backend.Repositories;
using Kakadu.Backoffice.Services;

namespace Kakadu.Backoffice.Views
{
    public class ProductManager : IManageable<Kakadu.Backend.Entities.Product>
    {
        private IProductRepository ProductRep;

        public ProductManager()
        {
            ProductRep = new ProductRepositoryXML();
        }

        public List<Backend.Entities.Product> LoadItems()
        {
            return ProductRep.GetAll();
        }

        public void AddItem(Backend.Entities.Product item)
        {
            ProductRep.Save(item);
        }

        public void DeleteItem(int Id)
        {
            ProductRep.DeleteById(Id);
        }

        public void EditItem(int itemId, Backend.Entities.Product newItem)
        {
            ProductRep.Update(itemId, newItem);
        }

    }
}
