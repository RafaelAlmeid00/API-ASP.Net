
public class HashPassword
{
    public static string HashGeneration(string password)
    {
        int workfactor = 10; 
        
        string salt = BCrypt.Net.BCrypt.GenerateSalt(workfactor);
        string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hash;
    }

    public static bool PasswordCompare(string hash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

}
