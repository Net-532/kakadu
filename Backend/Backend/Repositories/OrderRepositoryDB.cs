using Kakadu.Backend.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kakadu.Backend.Repositories
{
    public class OrderRepositoryDB : IOrderRepository
    {
        public void ChangeStatus(int id, string status)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE orders SET status = @status, updatedAt = CURRENT_TIMESTAMP WHERE id = @id", DatabaseConnection.GetInstance().GetConnection());
            cmd.Parameters.AddWithValue("status", status);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Order> GetAll()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT id, orderNumber, totalPrice, createdAt, updatedAt, status FROM orders", DatabaseConnection.GetInstance().GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Order> orders = new List<Order>();
            while (reader.Read())
            {
                var convertOrder = ConvertOrder(reader);
                orders.Add(convertOrder);
            }
            reader.Close();
            return orders;
        }

        private Order ConvertOrder(MySqlDataReader reader)
        {
            Order order = new Order
            {
                Id = reader.GetInt32("id"),
                OrderNumber = reader.GetInt32("orderNumber"),
                TotalPrice = reader.GetDecimal("totalPrice"),
                OrderDate = reader.GetDateTime("createdAt"),
                UpdatedAt = reader.GetDateTime("updatedAt"),
                Status = reader.GetString("status")
            };

            return order;
        }

        public List<Order> GetAllByUpdatedAt(DateTime from, DateTime to)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT id, orderNumber, totalPrice, createdAt, updatedAt, status FROM orders WHERE updatedAt BETWEEN @from AND @to", DatabaseConnection.GetInstance().GetConnection());
            cmd.Parameters.AddWithValue("from", from);
            cmd.Parameters.AddWithValue("to", to);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Order> orders = new List<Order>();
            while (reader.Read())
            {
                var convertOrder = ConvertOrder(reader);
                orders.Add(convertOrder);
            }
            reader.Close();
            return orders;
        }

        public Order GetById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT id, orderNumber, totalPrice, createdAt, updatedAt, status FROM orders WHERE id = @id", DatabaseConnection.GetInstance().GetConnection());
            cmd.Parameters.AddWithValue("id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var order = ConvertOrder(reader);
                reader.Close();
                return order;
            }
            reader.Close();
            return null;
        }

        public Order GetByNumber(int number)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT id, orderNumber, totalPrice, createdAt, updatedAt, status FROM orders WHERE orderNumber = @orderNumber", DatabaseConnection.GetInstance().GetConnection());
            cmd.Parameters.AddWithValue("orderNumber", number);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var order = ConvertOrder(reader);
                reader.Close();
                return order;
            }
            reader.Close();
            return null;
        }

        public Order Save(Order order)
        {
            if (order.Id == 0)
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO orders (totalPrice, createdAt, updatedAt, status) VALUES (@totalPrice, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, @status); SELECT LAST_INSERT_ID();", DatabaseConnection.GetInstance().GetConnection());
                cmd.Parameters.AddWithValue("totalPrice", order.TotalPrice);
                cmd.Parameters.AddWithValue("status", order.Status);
                order.Id = Convert.ToInt32(cmd.ExecuteScalar());

                
                MySqlCommand getOrderNumberCmd = new MySqlCommand("SELECT orderNumber FROM orders WHERE id = @id", DatabaseConnection.GetInstance().GetConnection());
                getOrderNumberCmd.Parameters.AddWithValue("id", order.Id);
                order.OrderNumber = Convert.ToInt32(getOrderNumberCmd.ExecuteScalar());
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE orders SET totalPrice = @totalPrice, status = @status, updatedAt = CURRENT_TIMESTAMP WHERE id = @id", DatabaseConnection.GetInstance().GetConnection());
                cmd.Parameters.AddWithValue("totalPrice", order.TotalPrice);
                cmd.Parameters.AddWithValue("status", order.Status);
                cmd.Parameters.AddWithValue("id", order.Id);
                cmd.ExecuteNonQuery();
            }
            return order;
        }
    }
}
