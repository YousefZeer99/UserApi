using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UsersAPI.Model;
using UsersAPI.Repos;
using AutoMapper;
using UsersAPI.ViewM;
using Microsoft.AspNetCore.Authorization;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        INewUserRepo _userService;
        IMapper _mapper;


        public UsersController(INewUserRepo service , IMapper mapper)
        {
            _userService = service;
            _mapper = mapper;
        }



        // for part 2 from the task i will pass the role as a parameter with the header

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserVM>>> GetAllUsers()
        {
            var listOfU = await _userService.Get<UserVM>();
            var userViewM = _mapper.Map<List<UserVM>>(listOfU);
            if (listOfU == null)
                return NotFound(); 
            return Ok(userViewM);
        }

        


        [HttpGet("{id}")]
        
        public async Task<ActionResult<UserVM>> GetUser(int id)
        {
            var user = await _userService.GetId<UserVM>(id);
            var userViewM = _mapper.Map<UserVM>(user);
            if (user == null)
                return NotFound();
            return Ok(userViewM);
        }

        //Update user
        [HttpPut]
        public ActionResult<UserVM> SaveUser(UserVM user)
        {
            var model=_userService.Update(_mapper.Map<User>(user),user.Id);
            var userViewM = _mapper.Map<List<UserVM>>(model);
            return Ok(userViewM);
        }

        // Add user
        [HttpPost]
        public async Task<ActionResult<UserVM>> Createuser(UserVM user)
        {
           var model = await _userService.Add(_mapper.Map<User>(user),user.Id);
            var userViewM = _mapper.Map<UserVM>(model); 
            return Ok(userViewM); 

        }


        [HttpDelete]
        public async Task<ActionResult<UserVM>> DeleteUser(int id)
        {
           await _userService.Delete<UserVM>(id);
            return Ok();
        }
    }
}
