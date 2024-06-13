using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

public class DatabaseConnection
{
    private static DatabaseConnection _instance = null;
    private static readonly object _lock = new object();
    private static readonly object _connlock = new object();
    private MySqlConnection _DBConnection;

    private DatabaseConnection()
    {
        string host = ConfigurationManager.AppSettings["DBHost"];
        string database = ConfigurationManager.AppSettings["DBName"];
        string port = ConfigurationManager.AppSettings["DBPort"];
        string user = ConfigurationManager.AppSettings["DBUser"];
        string password = ConfigurationManager.AppSettings["DBPassword"];
        string connectionString = $"Server={host};Database={database};Port={port};User ID={user};Password={password};Pooling=true;";

        _DBConnection = new MySqlConnection(connectionString);
    }

    public static DatabaseConnection GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new DatabaseConnection();
            }
            return _instance;
        }
    }

    public MySqlConnection GetConnection()
    {
        lock (_connlock)
        {
            if (_DBConnection.State != ConnectionState.Open)
            {
                _DBConnection.Open();
            }
            return _DBConnection;
        }
    }
}
