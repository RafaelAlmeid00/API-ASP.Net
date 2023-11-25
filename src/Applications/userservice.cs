
public class UserService : IUserService
{
    private readonly IDatabaseConnection _databaseConnection;

    public UserService(IDatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

      public void SignUp(User user, IDatabaseConnection databaseConnection)
    {
        user.SaveToDatabase(databaseConnection);
    }

      public void Login(User user, IDatabaseConnection databaseConnection)
    {
        user.Login(databaseConnection);
    }
}
