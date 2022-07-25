using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Model;
using UsersAPI.Repos;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        PostService _postservice;

        public PostController(PostService postService)
        {
            _postservice = postService;
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var posts = _postservice.GetPostsList();
            if (posts == null)
                return NotFound();
            return Ok(posts);
        }

        [HttpPost] 
        public IActionResult AddPost( [FromBody] Post post)
        {
            _postservice.AddPost(post); 
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetPostID(int id )
        {
            var post = _postservice.GetPostsId(id);
            if (post == null)
                return NotFound();
            return Ok(post); 
        }

        [HttpPut]
        public IActionResult UpdatePost(Post post)
        {
            _postservice.UpdatePost(post);
            return Ok(); 
        }

        [HttpDelete]
        public IActionResult DeletePost(int id)
        {
            _postservice.DeletePost(id) ;
            return Ok(); 

        }






    }
}
