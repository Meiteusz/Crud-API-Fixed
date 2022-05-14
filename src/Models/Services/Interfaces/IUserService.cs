namespace Models
{
    public interface IUserService
    {
        Response Save(User user);
        ResponseSingleData<User> GetById(int userId);
        ResponseQuery<User> GetAll();
        ResponseSingleData<User> Edit(User user);
        Response Delete(int userId);
    }
}