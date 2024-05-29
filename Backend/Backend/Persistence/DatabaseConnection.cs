using System;
using System.Data;
using MySql.Data.MySqlClient;

public class DatabaseConnection
{
    private static DatabaseConnection instance = null;
    private static readonly object _lock = new object();
    private static readonly object _connlock = new object();
    private MySqlConnection DBConnection;
    private string connectionString;

    private DatabaseConnection()
    {
        connectionString = $"Server=localhost;Database=kakadu;User ID=manager;Password=FN79GDgQ6PI6fgx;Pooling=true;";
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
        lock (_connlock)
        {
            try
            {
                if (DBConnection == null)
                {
                    DBConnection = new MySqlConnection(connectionString);
                    DBConnection.Open();
                }
                else if (DBConnection.State != ConnectionState.Open)
                {
                    DBConnection.Open();
                }
                return DBConnection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Oppsie something wrong with DBConnection ", ex.Message);
                return null;
            }
        }
    }
}
