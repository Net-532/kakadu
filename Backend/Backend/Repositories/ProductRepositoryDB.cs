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
            MySqlCommand command = new MySqlCommand("DELETE FROM kakadu.products WHERE ProductID = @id", connection);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
        }

        public List<Product> GetAll()
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("SELECT ProductID, Name, Description, Price, PhotoUrl FROM kakadu.products", connection);
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
            MySqlCommand command = new MySqlCommand("SELECT ProductID, Name, Description, Price, PhotoUrl FROM kakadu.products WHERE ProductID = @id", connection);
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
            MySqlCommand command = new MySqlCommand("INSERT INTO kakadu.products (Name, Description, Price, PhotoUrl) VALUES (@Name, @Description, @Price, @PhotoUrl)", connection);
            command.Parameters.AddWithValue("@Name", product.Title);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@PhotoUrl", product.PhotoUrl);
            command.ExecuteNonQuery();
        }

        public void Update(int id, Product product)
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("UPDATE kakadu.products SET Name = @Name, Description = @Description, Price = @Price, PhotoUrl = @PhotoUrl WHERE ProductID = @id", connection);
            command.Parameters.AddWithValue("@id", product.Id);
            command.Parameters.AddWithValue("@Name", product.Title);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@PhotoUrl", product.PhotoUrl);
            command.ExecuteNonQuery();
        }

        private Product ConvertToProduct(MySqlDataReader reader)
        {
            Product product = new Product
            {
                Id = reader.GetInt32("ProductID"),
                Title = reader.GetString("Name"),
                Description = reader.GetString("Description"),
                Price = reader.GetDecimal("Price"),
                PhotoUrl = reader.GetString("PhotoUrl")
            };
            return product;
        }
    }
}
