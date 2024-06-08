using Kakadu.Backend.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Kakadu.Backend.Repositories
{
    public class ProductRepositoryDB : IProductRepository
    {
        public void DeleteById(int id)
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("DELETE FROM kakadu.products WHERE id = @id", connection);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
        }

        public List<Product> GetAll()
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("SELECT id, title, price, photoUrl, description FROM kakadu.products", connection);
            MySqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                var product = ConvertToProduct(reader);
                products.Add(product);
            }
            reader.Close();
            return products;
        }

        public Product GetById(int id)
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("SELECT id, title, price, photoUrl, description FROM kakadu.products WHERE id = @id", connection);
            command.Parameters.AddWithValue("id", id);
            MySqlDataReader reader = command.ExecuteReader();

            Product product = null;

            if (reader.Read())
            {
                product = ConvertToProduct(reader);
            }
            reader.Close();
            return product;
        }

        public void Save(Product product)
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("INSERT INTO kakadu.products (title, description, price, photoUrl) VALUES (@title, @description, @price, @photoUrl)", connection);
            command.Parameters.AddWithValue("title", product.Title);
            command.Parameters.AddWithValue("description", product.Description);
            command.Parameters.AddWithValue("price", product.Price);
            command.Parameters.AddWithValue("photoUrl", product.PhotoUrl);
            command.ExecuteNonQuery();
        }

        public void Update(int id, Product product)
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("UPDATE kakadu.products SET title = @title, description = @description, price = @price, photoUrl = @photoUrl WHERE id = @id", connection);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("title", product.Title);
            command.Parameters.AddWithValue("description", product.Description);
            command.Parameters.AddWithValue("price", product.Price);
            command.Parameters.AddWithValue("photoUrl", product.PhotoUrl);
            command.ExecuteNonQuery();
        }

        private Product ConvertToProduct(MySqlDataReader reader)
        {
            Product product = new Product
            {
                Id = reader.GetInt32("id"),
                Title = reader.GetString("title"),
                Description = reader.GetString("description"),
                Price = reader.GetDecimal("price"),
                PhotoUrl = reader.GetString("photoUrl")
            };
            return product;
        }
    }
}
