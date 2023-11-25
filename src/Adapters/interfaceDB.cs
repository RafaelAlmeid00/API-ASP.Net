using MySql.Data.MySqlClient;

public interface IDatabaseConnection
{
    void Connect();
    void Disconnect();
    MySqlConnection GetConnection();
}
