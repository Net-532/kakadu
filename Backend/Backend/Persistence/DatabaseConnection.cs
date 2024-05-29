using System;
using MySql.Data.MySqlClient;

public class DatabaseConnection
{
    private static DatabaseConnection instance = null;
    private static readonly object _lock = new object();
    private string connectionString;

    private DatabaseConnection()
    {
        string password = GeneratePassword();
        connectionString = $"Server=localhost;Database=kakadu;User ID=manager;Password={password};Pooling=true;";
    }

    public static DatabaseConnection GetInstance()
    {
        lock (_lock)
        {
            if (instance == null)
            {
                instance = new DatabaseConnection();
            }
            return instance;
        }
    }

    public MySqlConnection GetConnection()
    {
        try
        {
            MySqlConnection DBConnection = new MySqlConnection(connectionString);
            DBConnection.Open();
            return DBConnection;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    private string GeneratePassword()
    {
        Random rand = new Random();
        return rand.Next(1000, 10000).ToString();
    }
}
