
using Microsoft.EntityFrameworkCore;
using UsersAPI.Model;
using UsersAPI.ViewModels;


namespace UsersAPI.Repos
{
    public class PostRepo:PostService
    {
        private UserContext _context;

        public PostRepo(UserContext context)
        {
            _context = context;
        }

        public List<Post> GetPostsList()
        {
            List<Post> posts;
            try
            {
                posts = _context.Set<Post>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return posts;
        }

        public Post GetPostsId(int Id)
        {
            Post posts;
            try
            {
                posts = _context.Find<Post>(Id);
            }
            catch (Exception)
            {
                throw;
            }
            return posts;
        }


        public ResponseModel AddPost(Post post)
        {
            ResponseModel result = new ResponseModel();
            _context.Add<Post>(post);
            result.IsSuccess = true;
            _context.SaveChanges();
            return result;

        }

        public ResponseModel UpdatePost(Post post)
        {


            ResponseModel model = new ResponseModel();
            Post TempPost = GetPostsId(post.Id);
            if (TempPost != null)
            {
                TempPost.Ptitle = post.Ptitle;
                

                _context.Update<Post>(TempPost);
                model.Messsage = "post has been updated";
            }

            _context.SaveChanges();
            model.IsSuccess = true;
            return model;
        }


        public ResponseModel DeletePost(int Id)
        {
            ResponseModel Model = new ResponseModel();
            Post post = GetPostsId(Id);
            if (post != null)
            {
                _context.Remove<Post>(post);
                _context.SaveChanges();
                Model.IsSuccess = true;
                Model.Messsage = "Success";
            }
            else
            {
                Model.IsSuccess = false;
                Model.Messsage = "NotFound";
            }
            return Model;
        }








    }
}
