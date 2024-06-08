using Kakadu.Backend.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Kakadu.Backend.Repositories
{
    public class OrderItemRepositoryDB : IOrderItemRepository
    {
        public List<OrderItem> GetAll()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT id, productId, orderId, quantity, price, amount FROM order_items", DatabaseConnection.GetInstance().GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            List<OrderItem> orderItems = new List<OrderItem>();
            while (reader.Read())
            {
                var orderItem = ConvertOrderItem(reader);
                orderItems.Add(orderItem);
            }
            reader.Close();
            return orderItems;
        }


        public List<OrderItem> GetByOrderId(int orderId)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT id, productId, orderId, quantity, price, amount FROM order_items WHERE orderId = @orderId", DatabaseConnection.GetInstance().GetConnection());
            cmd.Parameters.AddWithValue("@orderId", orderId);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<OrderItem> orderItems = new List<OrderItem>();
            while (reader.Read())
            {
                var orderItem = ConvertOrderItem(reader);
                orderItems.Add(orderItem);
            }
            reader.Close();
            return orderItems;
        }

        public OrderItem GetById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT id, productId, orderId, quantity, price, amount FROM order_items WHERE id = @id", DatabaseConnection.GetInstance().GetConnection());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var orderItem = ConvertOrderItem(reader);
                reader.Close();
                return orderItem;
            }
            reader.Close();
            return null;
        }

        public void Save(OrderItem orderItem)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO order_items (productId, orderId, quantity, price, amount) VALUES (@productId, @orderId, @quantity, @price, @amount)", DatabaseConnection.GetInstance().GetConnection());
            cmd.Parameters.AddWithValue("@productId", orderItem.ProductId);
            cmd.Parameters.AddWithValue("@orderId", orderItem.OrderId);
            cmd.Parameters.AddWithValue("@quantity", orderItem.Quantity);
            cmd.Parameters.AddWithValue("@price", orderItem.Price);
            cmd.Parameters.AddWithValue("@amount", orderItem.Amount);
            cmd.ExecuteNonQuery();
        }

        public void Update(OrderItem orderItem)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE order_items SET productId = @productId, orderId = @orderId, quantity = @quantity, price = @price, amount = @amount WHERE id = @id", DatabaseConnection.GetInstance().GetConnection());
            cmd.Parameters.AddWithValue("@productId", orderItem.ProductId);
            cmd.Parameters.AddWithValue("@orderId", orderItem.OrderId);
            cmd.Parameters.AddWithValue("@quantity", orderItem.Quantity);
            cmd.Parameters.AddWithValue("@price", orderItem.Price);
            cmd.Parameters.AddWithValue("@amount", orderItem.Amount);
            cmd.Parameters.AddWithValue("@id", orderItem.Id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM order_items WHERE id = @id", DatabaseConnection.GetInstance().GetConnection());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private OrderItem ConvertOrderItem(MySqlDataReader reader)
        {
            OrderItem orderItem = new OrderItem
            {
                Id = reader.GetInt32("id"),
                ProductId = reader.GetInt32("productId"),
                OrderId = reader.GetInt32("orderId"),
                Quantity = reader.GetInt32("quantity"),
                Price = reader.GetDecimal("price"),
                Amount = reader.GetDecimal("amount")
            };

            return orderItem;
        }
    }
}
