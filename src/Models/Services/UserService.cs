namespace Models
{
    public class UserService : IUserService
    {
        private readonly ICrudAPIContext _context;

        public UserService(ICrudAPIContext context)
        {
            this._context = context;
        }

        public Response Save(User user)
        {
            try
            {
                _context.Users.Add(user);
                var changedRecords = _context.SaveChanges();

                if (changedRecords > 0)
                {
                    return new Response() { Success = true, Message = "User registered" };
                }
                return new Response();
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }

        public ResponseSingleData<User> GetById(int userId)
        {
            try
            {
                var user = _context.Users.Where(x => x.Id == userId)
                                         .SingleOrDefault();

                if (user != null)
                {
                    return new ResponseSingleData<User>() { Success = true, Data = user };
                }
                return new ResponseSingleData<User>() { Message = "User not found" };
            }
            catch (Exception ex)
            {
                return new ResponseSingleData<User>() { Message = ex.Message };
            }
        }

        public ResponseQuery<User> GetAll()
        {
            try
            {
                var users = _context.Users.ToList();

                if (users.Count > 0)
                {
                    return new ResponseQuery<User>() { Success = true, Results = users };
                }
                return new ResponseQuery<User>() { Message = "No registered user" };
            }
            catch (Exception ex)
            {
                return new ResponseQuery<User>() { Message = ex.Message };
            }
        }

        public ResponseSingleData<User> Edit(User user)
        {
            try
            {
                _context.Users.Update(user);
                var changedRecords = _context.SaveChanges();

                if (changedRecords > 0)
                {
                    return new ResponseSingleData<User>() { Success = true, Message = "User updated", Data = user };
                }
                return new ResponseSingleData<User>() { Message = "User not updated" };
            }
            catch (Exception ex)
            {
                return new ResponseSingleData<User>() { Message = ex.Message };
            }
        }

        public Response Delete(int userId)
        {
            try
            {
                var user = _context.Users.Where(x => x.Id == userId)
                                         .FirstOrDefault();

                if (user != null)
                {
                    _context.Users.Remove(user);
                    var changedRecords = _context.SaveChanges();

                    if (changedRecords > 0)
                    {
                        return new Response() { Success = true, Message = "User deleted" };
                    }
                    return new Response() { Message = "User not deleted" };
                }

                return new Response() { Message = "User not found" };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }
    }
}
