// Db.cs
using System.Data.Common;
using MySql.Data.MySqlClient;

public static class dbConnection
{
    private const string ConnectionString = "Server=localhost;Database=warehouse;Uid=root;Pwd=1234;";

    public static DbConnection Connect()
    {
        return new MySqlConnection(ConnectionString);
    }
}
