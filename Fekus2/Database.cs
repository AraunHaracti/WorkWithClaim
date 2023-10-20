using MySql.Data.MySqlClient;

namespace Fekus2;

public static class Database
{
    private static MySqlConnection _connector;
    private static MySqlConnectionStringBuilder _connectionStringBuilder = new()
    {
        Server = "localhost",
        Port = 3306,
        Database = "claim",
        UserID = "root",
        Password = "password"
    };
    
    public static MySqlDataReader GetData(string sql)
    {
        MySqlCommand command = new MySqlCommand(sql, _connector);
        MySqlDataReader reader = command.ExecuteReader();

        return reader;
    }

    public static void SetData(string sql)
    {
        MySqlCommand command = new MySqlCommand(sql, _connector);
        command.ExecuteNonQuery();
    }

    public static void Open()
    {
        _connector = new MySqlConnection(_connectionStringBuilder.ConnectionString);
        _connector.Open();
    }
    
    public static void Exit()
    {
        _connector.Close();
    }
}