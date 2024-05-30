using System;
using System.Data;
using MySql.Data.MySqlClient;

public class DatabaseConnection
{
    private static DatabaseConnection _instance = null;
    private static readonly object _lock = new object();
    private static readonly object _connlock = new object();
    private MySqlConnection _DBConnection;
    private static readonly string _connectionString = "Server=localhost;Database=kakadu;User ID=manager;Password=FN79GDgQ6PI6fgx;Pooling=true;";

    private DatabaseConnection()
    {
        _DBConnection = new MySqlConnection(_connectionString);
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
