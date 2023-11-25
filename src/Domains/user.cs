using MySql.Data.MySqlClient;

public class User(
    string cpf,
    string rg,
    string name,
    string email,
    string password,
    string date,
    string cep,
    string uf,
    string district,
    string street,
    string number,
    string complement,
    string city,
    string idList,
    string phone,
    string idCostumer
) : IUser
{
    public string CPF { get; set; } = cpf;
    public string RG { get; set; } = rg;
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string Date { get; set; } = date;
    public string CEP { get; set; } = cep;
    public string UF { get; set; } = uf;
    public string District { get; set; } = district;
    public string Street { get; set; } = street;
    public string Number { get; set; } = number;
    public string Complement { get; set; } = complement;
    public string City { get; set; } = city;
    public string Type { get; set; }
    public string IDList { get; set; } = idList;
    public string Phone { get; set; } = phone;
    public string IDCostumer { get; set; } = idCostumer;

    public void SaveToDatabase(IDatabaseConnection databaseConnection)
    {
        try
        {
            databaseConnection.Connect();

            string query =
                @"INSERT INTO user (user_CPF, user_RG, user_nome, user_email, user_senha, user_nascimento, user_endCEP, user_endUF, user_endbairro, user_endrua, user_endnum, user_endcomplemento, user_endcidade, user_tipo, list_CPF_list_id, user_cel, user_idcli)
                    VALUES (@CPF, @RG, @Name, @Email, @Password, @Date, @CEP, @UF, @District, @Street, @Number, @Complement, @City, @Type, @IDList, @Phone, @IDCostumer)";

            using (MySqlCommand cmd = new MySqlCommand(query, databaseConnection.GetConnection()))
            {
                string hashpassword = HashPassword.HashGeneration(Password);

                cmd.Parameters.AddWithValue("@CPF", CPF);
                cmd.Parameters.AddWithValue("@RG", RG);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", hashpassword);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@CEP", CEP);
                cmd.Parameters.AddWithValue("@UF", UF);
                cmd.Parameters.AddWithValue("@District", District);
                cmd.Parameters.AddWithValue("@Street", Street);
                cmd.Parameters.AddWithValue("@Number", Number);
                cmd.Parameters.AddWithValue("@Complement", Complement);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@IDList", IDList);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@IDCostumer", IDCostumer);

                cmd.ExecuteNonQuery();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro ao cadastrar usuário no banco de dados: {ex.Message}");
            throw;
        }
        finally
        {
            databaseConnection.Disconnect();
        }
    }

     public void Login(IDatabaseConnection databaseConnection)
    {
        try
        {
            databaseConnection.Connect();

            string query =
                @"SELECT * FROM user WHERE user_CPF = @CPF";

            using (MySqlCommand cmd = new MySqlCommand(query, databaseConnection.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@CPF", CPF);

                cmd.ExecuteNonQuery();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro ao Logar usuário: {ex.Message}");
            throw;
        }
        finally
        {
            databaseConnection.Disconnect();
        }
    }

}
