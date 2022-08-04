using AutoMapper;
using UsersAPI.Model;

namespace UsersAPI.Repos
{
    public interface INewUserRepo : IGeneRepo<User>
    {

    }

    public class NewUserRepo : GeneRepo<User>, INewUserRepo
    {
        public NewUserRepo(UserContext context , IMapper mapper) : base(context,mapper)
        {
        }
    }
}
