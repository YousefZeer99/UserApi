using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UsersAPI.Model;
using UsersAPI.Repos;
using UsersAPI.ViewM;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        INewPostRepo _postservice;
        IMapper _mapper; 

        public PostController(INewPostRepo postService , IMapper mapper)
        {
            _postservice = postService;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<PostVM>>> GetAllPosts()
        {

            var userID = User.FindFirst(ClaimTypes.Sid)?.Value; 
            var posts = await _postservice.Get<PostVM>();

            var PostsUser = posts.Where(c => c.UId == int.Parse(userID));


            var PostViewM = _mapper.Map<List<Post>>(PostsUser);



            if (posts == null)
                return NotFound();
            return Ok(PostViewM);
        }

        [Authorize]
        [HttpPost] 
        public async Task<ActionResult<PostVM>> AddPost( [FromBody] PostVM post)
        {
            var userID = User.FindFirst(ClaimTypes.Sid)?.Value;
            post.UId = int.Parse(userID);   

            var model = await _postservice.Add(_mapper.Map<Post>(post));
            var PostViewM = _mapper.Map<PostVM>(model);
            return Ok(PostViewM);
        }


        [HttpGet]
        [Route("[action]")]

        public async Task<IActionResult> GetPageN(int size, int page, string s)
        {
            try
            {
                var posts = await _postservice.GetPageN(size, page, s);
                if (posts == null)
                    return NotFound("Try again ... !!"); 
            return Ok(_mapper.Map<List<PostVM>>(posts));
            }

            catch(Exception ex)
            {
                return BadRequest(); 
            }
        }




        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PostVM>> GetPostID(int id )
        {
            // need to use try catch 

            try
            {

                var userID = User.FindFirst(ClaimTypes.Sid)?.Value;
                var post = await _postservice.GetId<PostVM>(id);
                var PostViewM = _mapper.Map<PostVM>(post);

                if (post.UId == int.Parse(userID))
                {
                    return Ok(PostViewM);
                }
                return NotFound();
            }

            catch (Exception ex)
            {
                return NotFound("ID is invalid ... Try again ^_^"); 
            }
        }


        [Authorize]
        [HttpPut]
        public async Task<ActionResult<PostVM>> UpdatePost(PostVM post)
        {
            try
            {

                var postM = await _postservice.GetId<PostVM>(post.Id);
                var userID = User.FindFirst(ClaimTypes.Sid)?.Value;

                if (postM.UId == int.Parse(userID))
                {
                    post.UId = int.Parse(userID);

                    var model = _postservice.Update(_mapper.Map<Post>(post));
                    var PostViewM = _mapper.Map<PostVM>(model);
                    return Ok("Updated successfully");
                }
                return NotFound("Invalid!!!");
            }

            catch(Exception ex)
            {
                return NotFound(); 
            }
        }


        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<PostVM>> DeletePost(int id)
        {
            try
            {
                var userID = User.FindFirst(ClaimTypes.Sid)?.Value;
                var post = await _postservice.GetId<PostVM>(id);

                if (post.UId == int.Parse(userID))
                {
                    await _postservice.Delete<PostVM>(id);
                    return Ok("Deleted successfully");
                }
                return NotFound("Invalid !!!");
            }

            catch(Exception ex)
            {
                return NotFound(); 
            }

        }






    }
}
