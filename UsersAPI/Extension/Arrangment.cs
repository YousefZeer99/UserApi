using UsersAPI.Model;
using UsersAPI.Repos;
using Microsoft.EntityFrameworkCore;

namespace UsersAPI.Extension
{
    public static class Arrangment
    {
        public static void NeedExt(this IServiceCollection uServOb , ConfigurationManager uConfig)
        {
          

            uServOb.AddDbContext<UserContext>(d => d.UseSqlServer(uConfig.GetConnectionString("ConnectionString1")));
            uServOb.AddScoped<INewUserRepo, NewUserRepo>();
            uServOb.AddScoped<INewPostRepo, NewPostRepo>();

        }
    }
}
