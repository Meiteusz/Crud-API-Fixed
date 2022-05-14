using Microsoft.EntityFrameworkCore;

namespace Models
{
    public interface ICrudAPIContext
    {
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}