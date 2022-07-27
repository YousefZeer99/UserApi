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
        INewPostRepo _postservice;

        public PostController(INewPostRepo postService)
        {
            _postservice = postService;
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var posts = _postservice.Get();
            if (posts == null)
                return NotFound();
            return Ok(posts);
        }

        [HttpPost] 
        public IActionResult AddPost( [FromBody] Post post)
        {
            _postservice.Add(post); 
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetPostID(int id )
        {
            var post = _postservice.GetId(id);
            if (post == null)
                return NotFound();
            return Ok(post); 
        }

        [HttpPut]
        public IActionResult UpdatePost(Post post)
        {
            _postservice.Update(post);
            return Ok(); 
        }

        [HttpDelete]
        public IActionResult DeletePost(int id)
        {
            _postservice.Delete(id) ;
            return Ok(); 

        }






    }
}
