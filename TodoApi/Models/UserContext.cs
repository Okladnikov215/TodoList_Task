using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Users;

namespace TodoApi.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
