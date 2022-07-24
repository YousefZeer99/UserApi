using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UsersAPI.Model;
using UsersAPI.Repos;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        IUserService _userService;
        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users=_userService.GetUsersList();
            if (users == null)
                return NotFound();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPut]
        public IActionResult SaveUser(User user)
        {
            var model=_userService.SaveUser(user);
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Createuser(User user)
        {
            _userService.CreateNuser(user);
            return Ok(); 

        }


        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var model=_userService.DeleteUser(id);
            return Ok(model);
        }
    }
}
