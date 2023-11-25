using System.Data;
using MySql.Data.MySqlClient;

public class MySqlConnectionAdapter(
    string server,
    string database,
    string username,
    string password
) : IDatabaseConnection, IDisposable
{
    public MySqlConnection _connection;

    public void Connect()
    {
        string connectionString =
            $"Server={server};Database={database};Uid={username};Pwd={password};";

        try
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
            // Lidar com o erro conforme necessário
            throw;
        }
    }

    public MySqlConnection GetConnection()
    {
        return _connection;
    }

    public void Disconnect()
    {
        _connection.Close();
        _connection.Dispose();
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public void SignUp(User user)
    {
        try
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            string query =
                @"INSERT INTO user (user_CPF, user_RG, user_nome, user_email, user_senha, user_nascimento, user_endCEP, user_endUF, user_endbairro, user_endrua, user_endnum, user_endcomplemento, user_endcidade, user_tipo, list_CPF_list_id, user_cel, user_idcli)
                            VALUES (@CPF, @RG, @Name, @Email, @Password, @Date, @CEP, @UF, @District, @Street, @Number, @Complement, @City,  @Type, @IDList, @Phone, @IDCostumer)";

            using (MySqlCommand cmd = new MySqlCommand(query, _connection))
            {
                string hashpassword = HashPassword.HashGeneration(user.Password);

                cmd.Parameters.AddWithValue("@CPF", user.CPF);
                cmd.Parameters.AddWithValue("@RG", user.RG);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", hashpassword);
                cmd.Parameters.AddWithValue("@Date", user.Date);
                cmd.Parameters.AddWithValue("@CEP", user.CEP);
                cmd.Parameters.AddWithValue("@UF", user.UF);
                cmd.Parameters.AddWithValue("@District", user.District);
                cmd.Parameters.AddWithValue("@Street", user.Street);
                cmd.Parameters.AddWithValue("@Number", user.Number);
                cmd.Parameters.AddWithValue("@Complement", user.Complement);
                cmd.Parameters.AddWithValue("@City", user.City);
                cmd.Parameters.AddWithValue("@Type", user.Type);
                cmd.Parameters.AddWithValue("@IDList", user.IDList);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@IDCostumer", user.IDCostumer);

                cmd.ExecuteNonQuery();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro ao cadastrar usuário no banco de dados: {ex.Message}");
            // Lidar com o erro conforme necessário
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void GetUser(User user)
{
    try
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }

        string query = @"SELECT * FROM user WHERE user_CPF = @CPF";

        using (MySqlCommand cmd = new MySqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@CPF", user.CPF);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) 
                {
                    string nome = reader["user_CPF"].ToString();
                    string senha = reader["user_senha"].ToString();

                    Console.WriteLine($"Nome: {nome}, Idade: {senha}");
                }
            }
        }
    }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro ao pegar usuário: {ex.Message}");
            // Lidar com o erro conforme necessário
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }
}
