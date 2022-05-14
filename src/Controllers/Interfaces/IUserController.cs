using Models;

namespace Controllers
{
    public interface IUserController
    {
        Response RegisterUser(User user);
        ResponseSingleData<User> GetUserById(int userId);
        ResponseQuery<User> GetAllUsers();
        ResponseSingleData<User> EditUser(User user);
        Response DeleteUser(int userId);
    }
}