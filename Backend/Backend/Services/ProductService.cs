using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using System;
using System.Collections.Generic;

namespace Kakadu.Backend.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void deleteById(int id)
        {
            var product = productRepository.getById(id);
            if (product == null)
            {
                throw new ProductNotFoundException($"Продукт з вказаним id {id} не знайдено");
            }
            productRepository.deleteById(id);
        }

        public List<Product> getAll()
        {
            return productRepository.getAll();
        }

        public Product getById(int id)
        {
            var product = productRepository.getById(id);
            if (product == null)
            {
                throw new ProductNotFoundException($"Продукт з вказаним id {id} не знайдено");
            }
            return product;
        }

        public void save(Product product)
        {
            productRepository.save(product);
        }

        public void update(int id, Product product)
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
