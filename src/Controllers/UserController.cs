using Models;

namespace Controllers
{
    public class UserController : IUserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        public Response RegisterUser(User user)
        {
            return _userService.Save(user);
        }

        public ResponseSingleData<User> GetUserById(int userId)
        {
            return _userService.GetById(userId);
        }

        public ResponseQuery<User> GetAllUsers()
        {
            return _userService.GetAll();
        }

        public ResponseSingleData<User> EditUser(User user)
        {
            return _userService.Edit(user);
        }

        public Response DeleteUser(int userId)
        {
            return _userService.Delete(userId);
        }
    }
}