using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Models
{
    public class CrudAPIContext : DbContext, ICrudAPIContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                  .AddJsonFile("appsettings.json")
                                  .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CrudAPIDefaultConnection"));
        }
    }
}
