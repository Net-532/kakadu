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
            var product = productRepository.getById(id);
            if (product == null)
            {
                throw new ProductNotFoundException($"Продукт з вказаним id {id} не знайдено");
            }
            productRepository.deleteById(id);
        }

        public List<Product> GetAll()
        {
            return productRepository.getAll();
        }

        public Product GetById(int id)
        {
            var product = productRepository.getById(id);
            if (product == null)
            {
                throw new ProductNotFoundException($"Продукт з вказаним id {id} не знайдено");
            }
            return product;
        }

        public void Save(Product product)
        {
            productRepository.save(product);
        }

        public void Update(int id, Product product)
        {
            var existingProduct = productRepository.getById(id);
            if (existingProduct == null)
            {
                throw new ProductNotFoundException($"Продукт з вказаним id {id} не знайдено");
            }
            productRepository.update(id, product);
        }
    }
}
