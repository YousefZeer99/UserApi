using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var posts = await _postservice.Get<PostVM>();
            var PostViewM = _mapper.Map<List<PostVM>>(posts);
            if (posts == null)
                return NotFound();
            return Ok(PostViewM);
        }


        [HttpPost] 
        public async Task<ActionResult<PostVM>> AddPost( [FromBody] PostVM post)
        {
           var model = await _postservice.Add(_mapper.Map<Post>(post));
            var PostViewM = _mapper.Map<PostVM>(model);
            return Ok(PostViewM);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<PostVM>> GetPostID(int id )
        {
            var post = await _postservice.GetId<PostVM>(id);
            var PostViewM = _mapper.Map<PostVM>(post);

            if (post == null)
                return NotFound();
            return Ok(PostViewM); 
        }

        [HttpPut]
        public ActionResult<PostVM> UpdatePost(PostVM post)
        {
            var model  = _postservice.Update(_mapper.Map<Post>(post));
            var PostViewM = _mapper.Map<PostVM>(model);
            return Ok(PostViewM); 
        }

        [HttpDelete]
        public async Task<ActionResult<PostVM>> DeletePost(int id)
        {
           await _postservice.Delete<PostVM>(id) ;
            return Ok(); 

        }






    }
}
