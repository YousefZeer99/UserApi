using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 

namespace UsersAPI.Model
{
    public class UserContext : IdentityDbContext<User, UserRole, int>
    {
        public UserContext(DbContextOptions <UserContext> options):base(options)
        {

        }

        public DbSet<User>  users { get; set; }
        public DbSet<Post> posts { get; set;  }


    }
}
