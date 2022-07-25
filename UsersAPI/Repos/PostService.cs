using UsersAPI.Model;
using UsersAPI.ViewModels;
using System.Collections.Generic; 

namespace UsersAPI.Repos
{
    public interface PostService
    {
        List<Post> GetPostsList();

        Post GetPostsId(int Id);

        ResponseModel AddPost(Post post); 

        ResponseModel UpdatePost(Post post);

        ResponseModel DeletePost(int Id);



    }
}
