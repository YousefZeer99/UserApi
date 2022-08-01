using AutoMapper;
using UsersAPI.Model; 

namespace UsersAPI.ViewM
{
    public class ProfileVM : Profile
    {
        public ProfileVM ()
            {
            CreateMap<User, UserVM>().ReverseMap();

            CreateMap<Post, PostVM>().ReverseMap(); 

            }

    }
}
