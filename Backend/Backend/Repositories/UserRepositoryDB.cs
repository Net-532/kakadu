using Kakadu.Backend.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Kakadu.Backend.Repositories
{
    public class UserRepositoryDB : IUserRepository
    {
        public void DeleteById(int id)
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("delete from kakadu.users where id = @id", connection);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery(); 
        }

        public List<User> GetAll()
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("select  id, username, password, first_name, last_name from kakadu.users", connection);
            MySqlDataReader reader = command.ExecuteReader();

            List<User> users = new List<User>();

            while (reader.Read())
            {
                var user = ConvertToUser(reader);
                users.Add(user);
            }
            reader.Close();
            return users;
        }

        public User GetById(int id)
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("select  id, username, password, first_name, last_name from kakadu.users where id = @id", connection);
            command.Parameters.AddWithValue("id", id);
            MySqlDataReader reader = command.ExecuteReader();

            User user = null;

            if (reader.Read())
            {
                user =  ConvertToUser(reader);
               
            }
            reader.Close();
            return user;
        }

        public void Save(User user)
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("insert into kakadu.users (username, first_name, last_name, password) VALUES (@username, @password, @firstname, @lastname)", connection);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@firstname", user.FirstName);
            command.Parameters.AddWithValue("@lastname", user.LastName);
            command.ExecuteNonQuery();

            
        }

        public void Update(int id, User user)
        {
            MySqlConnection connection = DatabaseConnection.GetInstance().GetConnection();
            MySqlCommand command = new MySqlCommand("update kakadu.users set username = @username, password = @password, first_name = @firstname, last_name = @lastname WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@firstname", user.FirstName);
            command.Parameters.AddWithValue("@lastname", user.LastName);
            command.ExecuteNonQuery();
        }

        private User ConvertToUser(MySqlDataReader reader)
        {
          
           User user = new User
            {
                Id = reader.GetInt32("id"),
                Username = reader.GetString("username"),
                Password = reader.GetString("password"),
                FirstName = reader.GetString("first_name"),
                LastName = reader.GetString("last_name")
            };
            return user;
        }
    }
}