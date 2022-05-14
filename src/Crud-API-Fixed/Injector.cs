using Controllers;
using Models;

namespace Crud_API_Fixed
{
    public class Injector
    {
        public static void InjectService(IServiceCollection service)
        {
            service.AddScoped<ICrudAPIContext, CrudAPIContext>();

            service.AddScoped<IUserController, UserController>();
            service.AddScoped<IUserService, UserService>();
        }
    }
}
