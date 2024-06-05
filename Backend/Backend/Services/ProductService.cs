using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using System.Collections.Generic;

namespace Kakadu.Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void DeleteById(int id)
        {
            var product = productRepository.GetById(id);
            if (product == null)
            {
                throw new EntityNotFoundException($"Продукт з вказаним id {id} не знайдено");
            }
            productRepository.DeleteById(id);
        }

        public List<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            var product = productRepository.GetById(id);
            if (product == null)
            {
                throw new EntityNotFoundException($"Продукт з вказаним id {id} не знайдено");
            }
            return product;
        }

        public void Save(Product product)
        {
            productRepository.Save(product);
        }

        public void Update(int id, Product product)
        {
            var existingProduct = productRepository.GetById(id);
            if (existingProduct == null)
            {
                throw new EntityNotFoundException($"Продукт з вказаним id {id} не знайдено");
            }
            productRepository.Update(id, product);
        }
    }
}
