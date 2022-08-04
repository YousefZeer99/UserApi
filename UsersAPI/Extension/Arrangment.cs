using UsersAPI.Model;
using UsersAPI.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UsersAPI.Extension
{
    public static class Arrangment
    {
        public static void NeedExt(this IServiceCollection uServOb , ConfigurationManager uConfig)
        {
          

            uServOb.AddDbContext<UserContext>(d => d.UseSqlServer(uConfig.GetConnectionString("ConnectionString1")));
            uServOb.AddScoped<INewUserRepo, NewUserRepo>();
            uServOb.AddScoped<INewPostRepo, NewPostRepo>();


            uServOb.Configure<JWT>(uConfig.GetSection("JWT"));

           
       uServOb.AddIdentity<User, UserRole>()
         .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();

            // Adding Authentication
            uServOb.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = uConfig["JWT:ValidAudience"],
                    ValidIssuer = uConfig["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(uConfig["JWT:Secret"]))
                };
            });



        }
    }
}
