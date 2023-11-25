public interface IUserService
{
    void SignUp(User user, IDatabaseConnection _databaseConnection);

    void Login(User user, IDatabaseConnection _databaseConnection);
}
