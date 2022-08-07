using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Model;

namespace UsersAPI.Repos
{
    public interface INewPostRepo : IGeneRepo<Post>
    {
        public Task<List<Post>?> GetPageN(int size, int page, string s); 

    }

    public class NewPostRepo : GeneRepo<Post>, INewPostRepo
    {
        private UserContext userContext; 
        public NewPostRepo(UserContext context, IMapper mapper) : base(context , mapper)
        {
            userContext = context; 
        }

        public Task<List<Post>?> GetPageN(int size, int page, string s)
        {
            return userContext.Set<Post>().Where(C => C.Ptitle.Contains(s)).Skip<Post>(size * page).Take<Post>(size).ToListAsync(); 

        }



    }
}
